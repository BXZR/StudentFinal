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
	//用于遮罩的技能图片
	public Image theImageForSkillFront;
	//遮罩的颜色
	public Color colorForEffecting = Color.yellow;
	public Color colorForCooling = Color.black;

	void Start()
	{
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
				print (theChild.gameObject.name);
				theSkill = theChild.GetComponent<SkillBasic> ();

			}
		}
		if (theSkill != null)
			theSkill.theButton = this;
	}

	public void makeSkillOperate()
	{
		makeStart ();//因为Start未必能够完全初始化

		if(theSkill)
		    theSkill.UseTheSkill ();
	}

	void Update () 
	{
		if (!theSkill || !theImageForSkillFront)
			return;
		
		theSkill.makeTimeCanculate();

		if(theSkill.theStateNow == skillState.isReady)
			theImageForSkillFront.enabled = false;
	
		else if(theSkill.theStateNow == skillState.isUsing)
		{
			theImageForSkillFront.enabled = true;
			theImageForSkillFront.fillAmount = theSkill.theStatePercent;
			theImageForSkillFront.color = colorForEffecting;
		}

		else if(theSkill.theStateNow == skillState.isCooling)
		{
			theImageForSkillFront.enabled = true;
			theImageForSkillFront.fillAmount = theSkill.theStatePercent;
			theImageForSkillFront.color = colorForCooling;
		}
		//print ("theSkill.theStatePercent = " + theSkill.theStatePercent);
		//print ("theSkill.theStateNow = "+theSkill.theStateNow);
		
	}
}
