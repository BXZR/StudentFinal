using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSet : MonoBehaviour {

	//最开始的设定
	//设定完成之后自动销毁
	void Start () 
	{
		Invoke ("makeStart" , 0.05f);
	}

	void makeStart()
	{
		Application.targetFrameRate = 40;
		UIController.GetInstance ().ShowUI<HpBasicPanel> ();
		UIController.GetInstance ().ShowUI<PlayerActCanvas> ();
		Destroy (this);
	}


}
