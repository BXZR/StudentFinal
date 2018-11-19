using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是一个Player的信息控制脚本
//这一次的Player将会尽可能简化的方式进行设计
//数值尽可能少
public delegate void HpChangeHook(float adder);
public class Player : MonoBehaviour {

	//游戏人物的数值
	public float hpNow = 100f;//当前生命值
	public float hpMaxNow = 100f;//当前生命上限
	public float attackDamage = 10f;//当前攻击力

	public SkillBasic theSkillNow = null;
	//公有操作生命的时间
	public event HpChangeHook HpChanger;

	/// <summary>
	/// 外部操作生命改变的方法，这个方法全球唯一
	/// </summary>
	public void OnHpChange(float hpChangeAdder)
	{
		HpChanger (hpChangeAdder);
	}


	//私有操作生命的方法
	private void ChangeHp(float adder = 0f)
	{
		hpNow += adder;
		hpNow = Mathf.Clamp (hpNow, 0f , hpMaxNow);
	}


	//这个方法食欲带动作的技能触发事件联系在一起的
	public  void SkillEffect (float extradamage)
	{
		if (theSkillNow != null)
			theSkillNow.SkillEffect (extradamage);
		theSkillNow = null;
	}

	void Start () 
	{
		SystemValues.thePlayer = this.gameObject;
		HpChanger += this.ChangeHp;
	}

}
