﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSkill : SkillBasic {

	private CharacterController theMoveController;
	private animatorController theAnimatorController;
	private move moveOBJ;
	//跳跃力量
	[Range(10f,50f)]
	public float jumpForce = 25f;
	private float yMove = 0f;

	void Start ()
	{
		Init ();
		theMoveController = thePlayer.GetComponent<CharacterController> ();
		theAnimatorController = thePlayer.GetComponent<animatorController> ();
		moveOBJ = thePlayer.GetComponent<move> ();
		if (!theMoveController)
			Destroy (this);
	}

	public override void Init ()
	{
		skillName = "琼华派身法";//技能名字
		skillInformation = "没有御剑飞仙：\n向上跳跃一段距离。\n御剑飞仙：\n向正前方突进一段距离。\n冷却时间："+(skillAllTimer - skillEffectTime).ToString("f1")+"秒";//技能介绍
		skillAllTimer = 1f;//冷却时间
		skillEffectTime = 0.4f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
	}


	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;

		if(theAnimatorController && !(moveOBJ.theMoveModeNow is flyMoveMode) )
			theAnimatorController.PlayAnimation (playerAction.jump);
	}


	public override void OnCool ()
	{
		//动作结尾
		moveOBJ.theMoveModeNow.ExtraUpdate2End (moveOBJ);
	}

	public override void OnUpdate ()
	{
		if (theStateNow == skillState.isUsing)
		{
			yMove = (skillAllTimer - timerForAdd) * jumpForce * Time.deltaTime;
			moveOBJ.theMoveModeNow.ExtraUpdate2 (moveOBJ, yMove);
		}

		yMove = 0f;
	}

	void Update()
	{
		OnUpdate ();
	}


}
