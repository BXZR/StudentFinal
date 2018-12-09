using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBoss : Acter {

	//一般剧情上才会有的Boss
	//Boss一般都是需要经过对话之后才能够进入战斗的
	//并且Boss有全屏的血条而不是小的血条
	public int PlotType;
	public int PlotID;

	private FSMStage theAI;
	#region 动画与攻击事件
	/// <summary>
	/// 这个方法食欲带动作的技能触发事件联系在一起的
	/// 这个方法是自动Animator调用的
	/// </summary>
	public  void SkillEffect (float extradamage)
	{
		if(theAI.theStateNow.theAim)
			OnAttack(theAI.theStateNow.theAim);
	}

	#endregion

	#region 内部处理方法
	/// <summary>
	/// monster自身操作生命值的方法
	/// </summary>
	private void ChangeDamage(float adder = 0f)
	{
		attackDamage += adder;
	}

	/// <summary>
	/// monster自身操作生命值的方法
	/// </summary>
	private void ChangeHp(float adder = 0f)
	{
		hpNow += adder;
		hpNow = Mathf.Clamp (hpNow, 0f , hpMaxNow);
		if (adder < 0)
			theAI.GotAim ();
		if (hpNow == 0)
			OnDead ();
	}

	#endregion
	void Start () 
	{
		HpChanger += this.ChangeHp;
		DamageChanger += this.ChangeDamage;
		theAnimationController = this.GetComponent<Animator> ();
		theMoveController = this.GetComponent<move> ();
		theAI = this.GetComponent<FSMStage> ();
	}


	//Boss的血条是一个屏幕上的UISlider
	public override void MakeHpSlider ()
	{
		UIController.GetInstance ().ShowUI<BossHpSlider> (this.gameObject.name);
		this.theBloodSlider = null;
	}

	//boss战是有一个区域的，这个区域是不死不休的
	private GameObject theBossWall;

	private void MakeBossWall()
	{
		theBossWall = GameObject.Instantiate( SystemValues.LoadResources<GameObject>("Boss/BossWall"));
		theBossWall.transform.position = this.transform.position;
	}

	public void StartBoss()
	{
		MakeBossWall ();
		MakeHpSlider ();
		Invoke ("makeStart" , 0.25f);
	}

	private void makeStart()
	{
		this.GetComponent<FSMStage> ().enabled = true;
	}

	//boss被打倒是需要串剧情的
	public override void OnDead ()
	{
		base.OnDead ();

		string plotName = SystemValues.getPlotName (PlotType , PlotID);
		//print (plotName  +"---");
		if (string.IsNullOrEmpty (plotName))
			return;

		Destroy (this.theBossWall);
		UIController.GetInstance ().ShowUI<TalkCanvas> (plotName);

	}
		
}
