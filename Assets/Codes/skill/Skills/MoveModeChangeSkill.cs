using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModeChangeSkill: SkillBasic {

	private move moveOBJ;
	private animatorController theAnimatorController;
	public Sprite flySprite;
	public Sprite runSprite;
	private ETCButton theETCButton;

	void Start ()
	{
		Init ();

		moveOBJ = thePlayer.GetComponent<move> ();
	
		if (!moveOBJ )
			Destroy (this);

	}

	public override void Init ()
	{
		skillName = "切换运动状态";//技能名字
		skillInformation = "纵向位移";//技能介绍
		skillAllTimer = 0.6f;//冷却时间
		skillEffectTime = 0.1f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();

	}


	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;

		if (moveOBJ.theMoveModeNow is flyMoveMode) 
		{
			moveOBJ.changeMoveMode (new runMoveMode ());
			ChangePicture (runSprite);
		} 
		else 
		{
			moveOBJ.changeMoveMode (new flyMoveMode ());
			ChangePicture (flySprite);
		}
	}


	private void ChangePicture(Sprite aim)
	{
		if (!theETCButton)
			theETCButton = theButton.GetComponent<ETCButton> ();
		if (theETCButton) 
		{
			theETCButton.normalSprite = aim;
			theETCButton.pressedSprite = aim;
		}
	}




}
