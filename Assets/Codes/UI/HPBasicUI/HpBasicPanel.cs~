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
	public Text theDamageText;//战斗力数值
	private Player thePlayer;//游戏玩家

	void Start()
	{
		thePlayer = SystemValues.thePlayer.GetComponent<Player> ();
		theHpSlider.value =  thePlayer.hpNow / thePlayer.hpMaxNow;
		thePlayer.HpChanger += OnHpChange;
	}

	private void  OnHpChange(float hpadder)
	{
		theHpSlider.value =  thePlayer.hpNow / thePlayer.hpMaxNow;
	}
}
