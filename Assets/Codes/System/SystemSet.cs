using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSet : MonoBehaviour {

	public Transform theStartPosition;
	GameObject thePlayer;
	//最开始的设定
	//设定完成之后自动销毁
	void Start () 
	{
		thePlayer = (GameObject)GameObject.Instantiate (Resources.Load<GameObject>("Player/ThePlayer"));
		SystemValues.thePlayer = thePlayer;
		if (SystemValues.theSaveData == null)
			thePlayer.transform.position = theStartPosition.position;
		else
			SystemValues.makeTrueLoad ();
		Invoke ("makeStart" , 0.05f);
	}

	void makeStart()
	{
		loadGameInformation ();
		loadSetting ();
		Destroy (this);
	}

	/// <summary>
	/// 加载文档信息.
	/// </summary>
	void loadGameInformation()
	{
		SystemValues.LoadPlots ();


		Destroy (theStartPosition.gameObject);
	}

	/// <summary>
	/// 加载设定信息.
	/// </summary>
	void loadSetting()
	{
		Application.targetFrameRate = 40;
		UIController.GetInstance ().ShowUI<HpBasicPanel> ();
		UIController.GetInstance ().ShowUI<PlayerActCanvas> ();
	}


}
