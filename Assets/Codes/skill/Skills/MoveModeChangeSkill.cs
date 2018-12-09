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
		skillAllTimer = 0.6f;//冷却时间
		skillEffectTime = 0.1f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "御剑飞仙";//技能名字
		skillInformation = "御剑而飞，冯虚卓然，关山万里瞬息而至。\n冷却时间："+(skillAllTimer - skillEffectTime).ToString("f1")+"秒";//技能介绍

	}


	public override void UseTheSkill ()
	{
		if (canUseTheSkill ())
		{
			theStateNow = skillState.isUsing;
			thePlayer.theSkillNow = this;

			if (moveOBJ.theMoveModeNow is flyMoveMode) 
			{
				moveOBJ.changeMoveMode (new runMoveMode ());
				ChangePicture (runSprite);
				skillName = "御剑飞仙";//技能名字
				skillInformation = "御剑而飞，冯虚卓然，关山万里瞬息而至。\n冷却时间：" + (skillAllTimer - skillEffectTime).ToString ("f1") + "秒";//技能介绍
			} 
			else 
			{
				moveOBJ.changeMoveMode (new flyMoveMode ());
				ChangePicture (flySprite);
				skillName = "取消御剑";//技能名字
				skillInformation = "取消御剑飞仙，在凡尘经历人生，修仙修心。\n冷却时间：" + (skillAllTimer - skillEffectTime).ToString ("f1") + "秒";//技能介绍
			}
		}
	    else 
		UIController.GetInstance ().ShowUI<messageBox> ("暂时无法使用此技能");
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
