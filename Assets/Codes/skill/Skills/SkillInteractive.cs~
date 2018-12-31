using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInteractive : SkillBasic {

	//交互技能
	//这个技能的联动性非常强大
	//原理是相交球检查
	public float searchLength = 1f;
	public Sprite canInteractivePicture;
	public Sprite canNotInteractivePicture;
	ETCButton theETCButton;
	Image ButtonImage;

	void Start ()
	{
		Init ();

		//这个换图的功能在手机上始终有问题，所以实在不行就用这种方法规避一下
		//if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		    InvokeRepeating ("CheckCanInteractive" , 2f, 0.25f);
	}


	public override void Init ()
	{
		skillAllTimer = 4f;//冷却时间
		skillEffectTime = 0.1f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "万物通灵";//技能名字
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
		if (theStateNow == skillState.isCooling)
			return null;
		
		Collider[] interactiveAims = Physics.OverlapSphere (this.transform.position, searchLength);
		List<Collider> ams = new List<Collider> (interactiveAims);
		//print ("check--"+ams.Count);
		ams.RemoveAll (x => !x.tag .Equals( "Interactive"));

		if (ams.Count <= 0)
			return null;

		ams.Sort
		(
			(x ,y) =>  Vector3.Distance (x.transform.position, this.thePlayer.transform.position).CompareTo
			(Vector3.Distance (y.transform.position, this.thePlayer.transform.position))
		);
		return ams [0].GetComponent<InteractiveBasic> ();
	}

	/// <summary>
	/// 自动检查身边可以交互的物体，按钮图要随之改变
	/// </summary>
	void  CheckCanInteractive()
	{
		if (!theButton)
			return;
		
		if(!theETCButton)
			theETCButton = theButton.GetComponent<ETCButton> ();
		if (!ButtonImage)
			ButtonImage = theButton.GetComponent<Image> ();
		
		if (!theETCButton || !ButtonImage)
			return;
		
		
		InteractiveBasic aim = GetAim ();
		Sprite aimPicture = aim == null ? canNotInteractivePicture : canInteractivePicture;
		theETCButton.normalSprite = aimPicture;
		ButtonImage.sprite = aimPicture;

	}

}
