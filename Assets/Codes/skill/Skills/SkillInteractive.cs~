using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInteractive : SkillBasic {

	//交互技能
	//这个技能的联动性非常强大
	//原理是相交球检查
	public float searchLength = 1f;

	void Start ()
	{
		Init ();

	}

	public override void Init ()
	{
		skillAllTimer = 4f;//冷却时间
		skillEffectTime = 0.1f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "交互";//技能名字
		skillInformation = "与身边的人与事物进行交互，或许会得到有意思的收获。\n冷却时间：" + skillAllTimer.ToString("f1") + "秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;

		List<InteractiveBasic> IAs = new List<InteractiveBasic> ();
		Collider [] interactiveAims = Physics.OverlapSphere (this.transform .position, searchLength);
		InteractiveBasic aim = null;
		float distance = 9999f;
		for (int i=0; i<interactiveAims.Length; i++)
		{
			InteractiveBasic aimNew = interactiveAims [i].GetComponent<InteractiveBasic> ();
			if(aimNew)
			{
				print (aimNew.name);
				float distanceNew = Vector3.Distance (aimNew.transform.position, this.thePlayer.transform.position);
				if (distanceNew < distance) 
				{
					distance = distanceNew;
					aim = aimNew;
				}
			}
		}
		if (aim) {
			//print ("aim - "+aim.name);
			aim.MakeInteractive ();
			this.thePlayer.theMoveController.MakeLookAt (aim.transform);
		} 
		else
			UIController.GetInstance ().ShowUI<messageBox> ("附近没有可交互的事物");
	}
}
