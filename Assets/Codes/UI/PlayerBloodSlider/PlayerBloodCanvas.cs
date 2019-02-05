using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBloodCanvas : MonoBehaviour {


	public Slider theHpSlider;
	public Image theSliderFront;
	public Acter thePlayer;
	//主动关闭的标记
	private bool isAutoClose = false;
	private float closeTimer = 5f;
	private float closeTimerMax = 5f;


	void Start()
	{
		MakeAutoClose(3f);
	}

	void Update () 
	{
		this.transform.rotation = Camera.main.transform.rotation;

		if (isAutoClose)
		{
			closeTimer -= Time.deltaTime;
			if (closeTimer < 0) 
			{
				closeTimer = closeTimerMax;
				isAutoClose = false;
				this.gameObject.SetActive (false);
			}
		}
	}

	/// <summary>
	/// 挂在人物生命改变事件的方法
	/// </summary>
	/// <param name="theValue">The value.</param>
	public void ChangeSlider(float theValue)
	{
		theHpSlider.value = thePlayer.hpNow / thePlayer.hpMaxNow;
		showBloodText (theValue);
		if (thePlayer.hpNow <= 0)
			Destroy (this.gameObject);
	}


	//显示生命变化text
	private void showBloodText(float valueChange)
	{
		BloodChangeTextCanvas text = BloodChangeTextCanvas.GetText ();
		text.transform.SetParent (this.transform);
		text.MakeShow (valueChange);
	}

	//初始设定与刷新
	public void MakeFkash(Acter thePlayerIn)
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

	//有些血条是需要主动关闭的
	//例如游戏主人公的头顶血条
	public void MakeAutoClose(float timer = 5f)
	{
		closeTimer =  timer;
		isAutoClose = true;
		this.gameObject.SetActive (true);
	}
}
