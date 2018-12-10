using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInteractive : SkillBasic {

	//交互技能
	//这个技能的联动性非常强大
	//原理是相交球检查
	public float searchLength = 1f;
	public Sprite canInteractivePicture;
	public Sprite canNotInteractivePicture;
	ETCButton theETCButton;

	void Start ()
	{
		Init ();
		InvokeRepeating ("checkCanInteractive" , 1f , 0.25f);

	}


	public override void Init ()
	{
		skillAllTimer = 4f;//冷却时间
		skillEffectTime = 0.1f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "交互";//技能名字
		skillInformation = "与身边的人与事物进行交互，或许会得到有意思的收获。\n若身边有可交互的事物，会有相应的提示。\n冷却时间：" + skillAllTimer.ToString("f1") + "秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		if (canUseTheSkill ()) 
		{
			OnUse ();
			theStateNow = skillState.isUsing;
			thePlayer.theSkillNow = this;

			InteractiveBasic aim = GetAim ();
			if (aim)
			{
				//print ("aim - "+aim.name);
				aim.MakeInteractive ();
				this.thePlayer.theMoveController.MakeLookAt (aim.transform);
				this.thePlayer.OnInteractive (aim);
			} 
			else
				UIController.GetInstance ().ShowUI<messageBox> ("附近没有可交互的事物");
		} 
		else
			UIController.GetInstance ().ShowUI<messageBox> ("暂时无法进行交互");
	}

	/// <summary>
	/// 寻找交互的目标
	/// </summary>
	private InteractiveBasic GetAim()
	{
		List<InteractiveBasic> IAs = new List<InteractiveBasic> ();
		Collider[] interactiveAims = Physics.OverlapSphere (this.transform.position, searchLength);
		InteractiveBasic aim = null;
		float distance = 9999f;
		for (int i = 0; i < interactiveAims.Length; i++) 
		{
			InteractiveBasic aimNew = interactiveAims [i].GetComponent<InteractiveBasic> ();
			if (aimNew) 
			{
				//print (aimNew.name);
				float distanceNew = Vector3.Distance (aimNew.transform.position, this.thePlayer.transform.position);
				if (distanceNew < distance) 
				{
					distance = distanceNew;
					aim = aimNew;
				}
			}
		}
		return aim;
	}

	/// <summary>
	/// 自动检查身边可以交互的物体，按钮图要随之改变
	/// </summary>
	private void checkCanInteractive()
	{
		if (!theButton)
			return;
		
		if (!theETCButton)
			theETCButton = theButton.GetComponent<ETCButton> ();
		if (!theETCButton)
			return;

		InteractiveBasic aim = GetAim ();
		if (aim) 
			theETCButton.normalSprite = canInteractivePicture;
		else
			theETCButton.normalSprite = canNotInteractivePicture;
	}
}
