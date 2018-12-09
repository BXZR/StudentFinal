using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpSlider  : UIBasic
{
	public Slider theHpSlider;
	public Text theNametext;
	monsterBoss theBoss;

	public override void OnShow (string valueMonsterBoss = "")
	{
		GameObject boss = GameObject.Find (valueMonsterBoss);
		theBoss = boss.GetComponent <monsterBoss> ();
		theBoss.HpChanger += ChangeSlider;
		theNametext.text = theBoss.playerName;
	}
		

	//显示生命变化text
	private void showBloodText(float valueChange)
	{
		BloodChangeTextCanvas text = BloodChangeTextCanvas.GetText ();
		text.transform.SetParent (this.transform);
		text.MakeShow (valueChange);
	}

	public void ChangeSlider(float theValue)
	{
		theHpSlider.value = theBoss.hpNow / theBoss.hpMaxNow;
		showBloodText (theValue);
		if (theBoss.hpNow <= 0)
			Destroy (this.gameObject);
	}
}
