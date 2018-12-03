using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Attack : FSMBasic {


	//进行攻击
	private void  makeAttack()
	{
		if (Vector3.Distance (this.theMoveController.transform.position, this.theAim.transform.position) <= this.theAttackLength) 
		{
			this.theAnimator.Play ("attack");
		}
	}

	//看向目标
	private void makeLook()
	{
		if(this.theAim)
			this.theMoveController.transform.LookAt (this.theAim.transform);
	}

//真正的操作-----------------------------------------------------


	public override void actInThisState ()
	{
		//Debug.Log ("attack!");
		makeAttack ();
		makeLook ();
	}

	public override FSMBasic moveToNextState ()
	{
		if (this.theAim	 == null || this.theAim.isAlive == false) 
		{
			//Debug.Log ("attack to search");
			FSM_Search search = new FSM_Search ();
			search.makeState (this.theMoveController,this.theAnimator, this.theThis, null);
			return search;
		}
		if (Vector3.Distance (this.theMoveController.transform .position, this.theAim.transform .position) >  this.theAttackLength *0.8f)
		{
			Debug.Log ("attack to runafter");
			FSM_RunAfter runafter = new FSM_RunAfter ();
			runafter.makeState (this.theMoveController,this.theAnimator, this.theThis ,this.theAim );
			runafter.OnChangeToThisState ();
			return runafter;
		}
		return this;
	}
}
