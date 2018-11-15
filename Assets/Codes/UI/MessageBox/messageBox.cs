﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class messageBox : UIBasic {

	public Text theText;
	public float timer = 2f;
	public float timerMax = 2f;

 	public override void OnShow (string value = "")
	{
		theText.text = value;
		timer = timerMax;
	} 

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			timer = timerMax;
			this.gameObject.SetActive (false);
		}
	}
}