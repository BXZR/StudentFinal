using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Search : FSMBasic {

	List<GameObject> theEMYGet   = null;
	//public float angle = 125;//视野角度范围的一半
	//public float distance = 2.5f;//视野长度
	Acter theMainEMY = null;

	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	float searchTimer = 1f;
	float searchTimerMax = 1f;

	void searchAIMs()//不使用射线而是使用向量计算方法
	{
		searchTimer -= Time.deltaTime;
		if(searchTimer <0)
		{
			searchTimer = searchTimerMax;

			theEMYGet = SystemValues.searchAIMs (theViewAreaAngel, theSearchLength , this.theMoveController.transform );
			theMainEMY = getMainEMY ();
		}
	}


    //找到的目标很多，排序找到最终的目标
	private Acter getMainEMY()
	{
		//Debug.Log ("first check count = "+ theEMYGet.Count);
		for (int i = 0; i < theEMYGet.Count; i++) 
		{
			Acter thePlayer = theEMYGet [i].GetComponent <Acter> ();
			if (!theEMYGet [i].tag .Equals("AI") && thePlayer && thePlayer.isAlive)
				return thePlayer;
		}
		return null;
	}


	#region 移动控制
	private Vector3 randomAimPosition = Vector3.zero;
	float moveTimer = 9f;//每隔一段时间做一次就行
	float moveTimerMax = 9f;//时间间隔备份

	private void randomMove()
	{
		if (!this.theMoveController)
			return;
		
		moveTimer -= Time.deltaTime;

		Vector3 thisCheckVector = new Vector3 (this.theMoveController.transform.position.x, 0, this.theMoveController.transform.position.z);
		Vector3 randomCheckVector = new Vector3 (randomAimPosition.x, 0, randomAimPosition.z);
		bool reachCheck = (randomAimPosition == Vector3.zero  || Vector3.Distance (thisCheckVector,randomCheckVector)<0.3f) ;
		//Debug.Log ("distance => "+ Vector3.Distance (thisCheckVector,randomCheckVector));
		//Debug.Log(thisCheckVector +" ==== "+ randomCheckVector);
		//路径控制
		if (moveTimer < 0 && theMoveController)
		{
			moveTimer =  moveTimerMax;

			randomAimPosition = this.theMoveController.transform.position + new Vector3 (Random.value - 0.5f , Random.Range (0f, 2f), Random.value - 0.5f  );
			if(theMoveController.isActiveAndEnabled && theMoveController.isOnNavMesh)
		       theMoveController.SetDestination (randomAimPosition);
			//Debug.Log ("change route ==>"+ randomAimPosition );
		}
		//动画控制
		string animationName = reachCheck? "idle": "run";
		theAnimator.Play (animationName);

	}
	#endregion

//-------------------------------------------------------------------------//

	public override void actInThisState ()
	{
		//Debug.Log ("search!");
		searchAIMs ();
		randomMove ();
	}

	public override FSMBasic moveToNextState ()
	{
		//找不到目标就继续找
		if (theMainEMY == null)
			return this;
		//找到了目标就转到下一个状态
		else 
		{
			//Debug.Log ("search to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController, this.theAnimator, this.theThis,theMainEMY);
			attack.OnChangeToThisState ();
			return attack;
		}
 
	}

	public override void OnChangeToThisState ()
	{
		theMoveController.SetDestination (theMoveController.transform.position);
	}
}
