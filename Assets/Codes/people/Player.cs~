using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是一个Player的信息控制脚本
//这一次的Player将会尽可能简化的方式进行设计
//数值尽可能少

public class Player : Acter {

	//游戏人物的数值
	public float learningValue = 0f;//当前经验值
	public float learningValueMax = 100f;//当前经验上限


	//当前的技能
	public SkillBasic theSkillNow = null;
	//r任务背包，也就是任务控制单元
	public MissionPackage theMissionPackage;
	//经验值是玩家角色才会有的
	public event LearningChangeHook LearningChanger;



	#region 动画与攻击事件
	/// <summary>
	/// 这个方法食欲带动作的技能触发事件联系在一起的
	/// 这个方法是自动Animator调用的
	/// </summary>
	public  void SkillEffect (float extradamage)
	{
		if (theSkillNow != null)
			theSkillNow.SkillEffect (extradamage);
		theSkillNow = null;
	}

	public override void OnAttack(Acter aim)
	{
		if (!aim.isAlive)
			return;

		aim.OnHpChange (-this.attackDamage);
		if (aim.hpNow == 0) 
		{
			OnKill (aim);
			OnGetLearningValue (aim.lvNow * 15f);
		}
	}
	#endregion

	#region 外部数值管理事件
	/// <summary>
	/// 外部调用的获取经验的方法
	/// </summary>
	public void OnGetLearningValue(float adder)
	{
		LearningChanger (adder);
	}

	/// <summary>
	/// 刷新数值，更改显示
	/// 因为都是事件驱动的，没有一直查询，只好手动更新了
	/// </summary>
	public override void MakeFlash()
	{
		OnDamageChange (0f);
		OnGetLearningValue (0f);
		OnHpChange (0f);
	}
	#endregion

	#region 内部处理方法
	/// <summary>
	/// Player自身操作生命值的方法
	/// </summary>
	private void ChangeDamage(float adder = 0f)
	{
		attackDamage += adder;
	}
		
	/// <summary>
	/// Player自身操作经验值和升级的方法
	/// </summary>
	private void ChangeLearning(float adder = 0f)
	{
		learningValue += adder;
		while (learningValue > learningValueMax) 
		{
			learningValue -= learningValueMax;
			OnLvHp ();
			UIController.GetInstance ().ShowUI<messageBox> ("等级提升！");
		}
	}

	/// <summary>
	/// Player自身操作生命值的方法
	/// </summary>
	private void ChangeHp(float adder = 0f)
	{
		hpNow += adder;
		hpNow = Mathf.Clamp (hpNow, 0f , hpMaxNow);
		if (hpNow == 0)
			OnDead ();
	}

	/// <summary>
	/// 玩家升级了之后的数值修改管理
	/// </summary>
	private void OnLvHp()
	{
		lvNow += 1;
		hpMaxNow += 20f;//增加一点生命上限
		learningValueMax += 20f;//增加经验上限
		OnDamageChange (3f);//增加一点战斗力，顺带修改UI
		OnHpChange (10f);//恢复一点生命，顺带修改UI

	}


	public override void OnDead ()
	{
		isAlive = false;
		theAnimationController.Play ("dead");
		UIController.GetInstance ().ShowUI<messageBox> ("胜败乃兵家常事，大侠请重新来过");
		UIController.GetInstance ().ShowUI<UILoading> ("Start");

	}
		
	#endregion
	void Start () 
	{
		HpChanger += this.ChangeHp;
		DamageChanger += this.ChangeDamage;
		LearningChanger += this.ChangeLearning;
		theAnimationController = this.GetComponent<Animator> ();
		theMoveController = this.GetComponent<move> ();
		theMissionPackage = this.GetComponent<MissionPackage> ();
		MakeHpSlider ();
	}
		
}
