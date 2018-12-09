﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSkipWall : MonoBehaviour {

	//跳转场景的专用墙体
	public string aimScene = "";
	public int minMainPlotID = 0;//能够通过这里的最小主线ID
	void OnTriggerEnter(Collider collisioner)
	{
		if (collisioner.tag == "Player") 
		{
			if (SystemValues.getPlotID() >= minMainPlotID) 
			{
				SystemValues.SaveCatch ();
				Destroy (this);
				UIController.GetInstance ().ShowUI<UILoading> (aimScene);
			}
			else
			{
				UIController.GetInstance ().ShowUI<messageBox> ("目前此处无法通行");
			}
		}
	}

}