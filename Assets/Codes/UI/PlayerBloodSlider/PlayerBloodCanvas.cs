using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBloodCanvas : MonoBehaviour {


	public Slider theHpSlider;
	public Image theSliderFront;
	public Player thePlayer;

	void Update () 
	{
		this.transform.rotation = Camera.main.transform.rotation;
	}

	/// <summary>
	/// 挂在人物生命改变事件的方法
	/// </summary>
	/// <param name="theValue">The value.</param>
	public void ChangeSlider(float theValue)
	{
		theHpSlider.value = thePlayer.hpNow / thePlayer.hpMaxNow;
		if (thePlayer.hpNow <= 0)
			Destroy (this.gameObject);
	}

	//初始设定与刷新
	public void MakeFkash(Player  thePlayerIn)
	{
		SystemValues.bloodCamvasList.Add (this);
		this.thePlayer = thePlayerIn;
		this.thePlayer.HpChanger += ChangeSlider;
		ChangeSlider(0f);

		if (this.thePlayer.gameObject == SystemValues.thePlayer)
			theSliderFront.color = Color.green;
		else
			theSliderFront.color = Color.red;
	}
}
