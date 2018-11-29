using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBasicPanel : UIBasic {

	//这个UI用来显示生命值等信息
	//这个UI在最上面
	//游这一次游戏设计力求简单，数值计算统统不管，攻防属性也没有

	public Image theHeadImage;//头像
	public Slider theHpSlider;//生命值
	public Slider theLearningSlider;//经验
	public Text theDamageText;//战斗力数值
	public Text theLvText;//等级Text文本
	private Player thePlayer;//游戏玩家

	void Start()
	{
		//获取玩家或者其他组件
		thePlayer = SystemValues.thePlayer.GetComponent<Player> ();

		//初始显示
		theHpSlider.value =  thePlayer.hpNow / thePlayer.hpMaxNow;
		theDamageText.text = thePlayer.attackDamage.ToString ("f0"); 
		theLvText.text = thePlayer.lvNow.ToString();
		theLearningSlider.value = thePlayer.learningValue / thePlayer.learningValueMax;

		//注册事件
		thePlayer.HpChanger += OnHpChange;
		thePlayer.DamageChanger += OnDamageChange;
		thePlayer.LearningChanger += OnChangeLearning;
	}

	/// <summary>
	/// 玩家生命值发生改变的时候就会触发这个方法
	/// 这个是事件驱动的方法，一般不会直接调用
	/// </summary>
	private void  OnHpChange(float hpadder)
	{
		theHpSlider.value =  thePlayer.hpNow / thePlayer.hpMaxNow;
	}

	/// <summary>
	/// 玩家战斗力发生改变的时候就会触发这个方法
	/// 这个是事件驱动的方法，一般不会直接调用
	/// </summary>
	private void  OnDamageChange(float damageAdder)
	{
		theDamageText.text = thePlayer.attackDamage.ToString ("f0"); 
	}

	/// <summary>
	/// 玩家的经验等级显示
	/// </summary>
	private void OnChangeLearning(float adder)
	{
		theLvText.text = thePlayer.lvNow.ToString();
		theLearningSlider.value = thePlayer.learningValue / thePlayer.learningValueMax;
	}

}
