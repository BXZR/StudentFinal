using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是一个Player的信息控制脚本
//这一次的Player将会尽可能简化的方式进行设计
//数值尽可能少
public delegate void HpChangeHook(float adder);
public delegate void DamageChangeHook(float adder);
public delegate void LearningChangeHook(float adder);

public class Player : MonoBehaviour {

	//游戏人物的数值
	public float hpNow = 100f;//当前生命值
	public float hpMaxNow = 100f;//当前生命上限
	public float attackDamage = 4f;//当前攻击力
	public float learningValue = 0f;//当前经验值
	public float learningValueMax = 100f;//当前经验上限
	public int lvNow = 1;//当前等级

	public SkillBasic theSkillNow = null;
	//公有操作生命的事件
	public event HpChangeHook HpChanger;
	//公有操作战斗力的事件
	public event DamageChangeHook DamageChanger;
	//公有操作经验的事件
	public event LearningChangeHook LearningChanger;



	public void OnAttack(Player aim)
	{
		aim.OnHpChange (-this.attackDamage);
		if (aim.hpNow == 0)
			OnGetLearningValue (aim.lvNow * 15f);
	}


	/// <summary>
	/// 外部调用的获取经验的方法
	/// </summary>
	public void OnGetLearningValue(float adder)
	{
		LearningChanger (adder);
	}
		
	/// <summary>
	/// 外部操作生命改变的方法，这个方法全球唯一
	/// 所有新操作生命的方法都由内部事件处理.
	/// </summary>
	public void OnHpChange(float hpChangeAdder)
	{
		HpChanger (hpChangeAdder);
	}

	/// <summary>
	/// 外部操作战斗力的方法，这个方法全球统一
	/// 所有新操作战斗力的方法都由内部事件处理.
	/// </summary>
	public void OnDamageChange(float damageChangeAdder)
	{
		DamageChanger (damageChangeAdder);
	}

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

	/// <summary>
	/// 如果玩家挂了，自动调用这个方法后续处理
	/// </summary>
	private void OnDead()
	{
		print ("玩家虽然挂了，然而什么都没有发生，真实见了鬼了");
	}

	void Start () 
	{
		if (this.gameObject.tag == "Player" && SystemValues.thePlayer == null) 
		{
			SystemValues.thePlayer = this.gameObject;
		}
		HpChanger += this.ChangeHp;
		DamageChanger += this.ChangeDamage;
		LearningChanger += this.ChangeLearning;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Z))
			OnGetLearningValue (200f);

		if (Input.GetKeyDown (KeyCode.X))
			OnHpChange(-4f);

		if (Input.GetKeyDown (KeyCode.C))
			OnDamageChange(2f);

	}

}
