﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_RunAfter : FSMBasic {

	float timer = 4f;

	public override void OnFSMStateStart ()
	{
		this.theMoveController.enabled = true;
		OnChangeToThisState ();
	}

	public override void OnFSMStateEnd ()
	{
		timer = 1f;//时间刷新
	}

	public override void actInThisState ()
	{
		//Debug.Log ("run after");
		theAnimator.Play ("run");
		if (theAim) 
		{
			this.theMoveController.transform.LookAt (theAim.transform.position);
			this.theMoveController.SetDestination (theAim.transform.position + new Vector3 (Random.value*1.2f, 0, Random.value*1.2f));
		}
		timer -= Time.deltaTime;
		//Debug.Log ("runafterTimer : "+ timer);
	}

	public override FSMBasic moveToNextState ()
	{

		//为了确保能真的攻击到，留下多余空间
		//this.theThis.theAttackAreaLength*1.5f在没有遇到之前就开始会挥舞兵器了
		//至于移动就在于navMeshAgent了
		if (this.theAim && Vector3.Distance (this.theMoveController.transform.position , this.theAim.transform .position) <=  this.theAttackLength *1.5f)
		{
			//Debug.Log ("runafter to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController,this.theAnimator, this.theThis,this.theAim);
			attack.OnChangeToThisState ();
			return attack;
		}
		//没找到目标。这个状态损失得更快
		if(!theAim)
		{
			timer -= Time.deltaTime;
		}
		//Debug.Log ("theEMY name is "+ theEMY.name);
		if (timer < 0)
		{
			//Debug.Log ("runafter to search");
			FSM_Search search = new FSM_Search ();
			search.makeState (this.theMoveController,this.theAnimator, this.theThis , this.theAim);
			search.OnChangeToThisState ();
			return search;
		}
		return this;
	}
		
	//AI最简单的群体行为操作
	//一人追杀则群体追杀
	public override void OnChangeToThisState ()
	{

	}
}
