using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : InteractiveBasic{


	void Start()
	{
		SetTag ();
	}
	//收集物，这是一种标记
	public override void MakeInteractive ()
	{
		UIController.GetInstance ().ShowUI<messageBox> (this.InterName);
	}
}
