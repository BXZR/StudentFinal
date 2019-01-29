using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterRush : MonoBehaviour {

	//通用：怪物冲击技能
	//这类技能一般是叠加在FSMStage里面进行的
	private FSMStage theAI;
	private bool isRushing = false;
	private float rushTimer = 0.2f;
	private float rushTimerMax = 0.2f;
	public float damage = 7f;
	void Start ()
	{
		theAI = this.GetComponent<FSMStage> ();
		if (!theAI)
			Destroy (this);

		InvokeRepeating ("MakeRush" , 5f , Random.Range(6f,8f) );
	}

	private void MakeRush()
	{
		//只有在攻击的时候触发
		if (!(theAI.theStateNow is FSM_Attack))
			return;
		isRushing = true;
		this.theAI.enabled = false;
		this.GetComponent<BoxCollider> ().isTrigger = true;
	}
	 

	void Update () 
	{
		if (isRushing) 
		{
			this.transform.Translate (new Vector3 (0f,0f,1f) * 15f *Time.deltaTime);
			rushTimer -= Time.deltaTime;
			if (rushTimer < 0) 
			{
				isRushing = false;
				rushTimer = rushTimerMax;
				if (this.theAI.theStateNow.theAim) 
				{
					this.transform.LookAt (this.theAI.theStateNow.theAim.transform.position);
					this.theAI.enabled = true;
					this.GetComponent<BoxCollider> ().isTrigger = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider collisioner)
	{
		if (collisioner.tag == "Player" && isRushing) 
		{
			collisioner.GetComponent<Player> ().OnHpChange (-damage);

		}

	}
}
