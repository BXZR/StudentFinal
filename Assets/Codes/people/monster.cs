using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : Acter {


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
		MakeHpSlider ();
	}

}
