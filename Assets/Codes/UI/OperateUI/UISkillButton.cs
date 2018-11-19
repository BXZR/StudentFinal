using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//技能控制按钮
using UnityEngine.UI;

public enum SkillFindType {transform , component}
public class UISkillButton : MonoBehaviour {


	public SkillFindType findType = SkillFindType.component;
	public string skillName = "";//用名字来寻找技能
	//被控制的技能
	private SkillBasic theSkill = null;
	//真正的按钮
	private ETCButton theButton;

	//用于遮罩的技能图片
	public Image theImageForSkillFront;
	//遮罩的颜色
	public Color colorForEffecting = Color.yellow;
	public Color colorForCooling = Color.black;

	void Start()
	{
		theButton = this.GetComponent<ETCButton> ();
		makeStart ();
	}


	void makeStart()
	{
		if (theSkill != null)
			return;
		
		if (SystemValues.thePlayer != null) 
		{
			if (findType == SkillFindType.component)
				theSkill = (SkillBasic)SystemValues.thePlayer.GetComponentInChildren (System.Type.GetType (skillName));
			else if (findType == SkillFindType.transform) 
			{
				print (skillName);
				Transform theChild = GameObject.Find (skillName).transform;
				//print (theChild.gameObject.name);
				theSkill = theChild.GetComponent<SkillBasic> ();

			}
		}
		if (theSkill != null)
		{
			theSkill.theButton = this;
			theButton.normalSprite = theSkill.theSkillSprite;
			theButton.pressedSprite = theSkill.theSkillSprite;
			theButton.pressedColor = Color.gray;
		}
	}

	public void makeSkillOperate()
	{
		makeStart ();//因为Start未必能够完全初始化

		if (theSkill && theSkill.canUseTheSkill ()) 
		{
			theSkill.OnUse ();
			theSkill.UseTheSkill ();
		}
		else
		{
			UIController.GetInstance ().ShowUI<messageBox> ("当前无法使用该技能");
		}
	}


	/// <summary>
	///技能按钮UIButton的自动刷新工作.
	/// </summary>
	public void MakeFlash()
	{
		if (!theSkill || !theImageForSkillFront || theSkill.theStateNow == skillState.isReady)
			return;

		theSkill .timerForAdd += Time.deltaTime;

		if(theSkill.theStateNow == skillState.isUsing)
		{
			if (theSkill.timerForAdd < theSkill.skillEffectTime)
			{
				theSkill.theStatePercent = theSkill.timerForAdd / theSkill.skillEffectTime;
				theImageForSkillFront.fillAmount = theSkill.theStatePercent;
				theImageForSkillFront.enabled = true;
				theImageForSkillFront.color = colorForEffecting;
			}
			else
			{
				theSkill .theStateNow = skillState.isCooling;
				theSkill .OnCool ();
				theImageForSkillFront.color = colorForCooling;
			}

		}

		else if(theSkill.theStateNow == skillState.isCooling)
		{
			if (theSkill.timerForAdd < theSkill.skillAllTimer)
			{
				theSkill.theStatePercent = 1f - (theSkill.timerForAdd - theSkill.skillEffectTime) / (theSkill.skillAllTimer - theSkill.skillEffectTime);
				theImageForSkillFront.fillAmount = theSkill.theStatePercent;
			}
			else
			{
				theSkill .theStateNow = skillState.isReady;
				theSkill .OnReady ();
				theImageForSkillFront.enabled = false;
			}
		}
	}

	void Update () 
	{
		MakeFlash ();
	}
}
