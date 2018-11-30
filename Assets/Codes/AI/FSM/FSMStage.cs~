using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FSMStage :MonoBehaviour  {

	private NavMeshAgent theMoveController;//AI人物的移动控制类

	public FSMBasic theStateNow ;
	private Acter thethis; 
	private Animator theAnimator;
	public float AIThinkTimer = 0.20f;//AI每隔一段时间再进行思考
	//这是一个值得优化的点，在一些条件下关闭掉AI计算会非常省事
	//AI的计算很重并且同时计算的很多
	public bool theAiIsActing = true;//AI是否计算的标记

	//仇恨时间
	public float angerTimer = 4f;//仇恨时间，也是追击的总时长
	public float angetTimerMax = 4f;//仇恨时间上限

	bool isDeadMake = false;

	void Start ()
	{
		this.transform.root.tag = "AI";//打上标记方便找

		thethis = this.GetComponent <Acter> ();
		theMoveController = this.GetComponentInChildren<NavMeshAgent> ();
		theAnimator = this.GetComponentInChildren<Animator> ();

		theStateNow = new FSM_Search ();
		theStateNow.makeState (theMoveController , theAnimator ,5f , null );
		theStateNow.OnFSMStateStart ();
	}
		

	//有关AI计算状态-----------------------------------------------------------

    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
		//出于优化考虑不必让AI一直计算下去
		//此外这也可是“怪物僵直”状态的一个做法
		if (theAiIsActing && thethis.isAlive && theStateNow != null)
		{
			theStateNow.actInThisState ();
			theStateNow = theStateNow.moveToNextState ();
		}
	}
}
