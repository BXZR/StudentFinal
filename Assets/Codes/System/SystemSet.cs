using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSet : MonoBehaviour {

	public Transform theStartPosition;
	public int PlotType = -1;
	public int PlotID = -1;
	GameObject thePlayer;
	//最开始的设定
	//设定完成之后自动销毁
	void Start () 
	{
		GameObject playerData = SystemValues.LoadResources<GameObject>("Player/ThePlayer");
		thePlayer = (GameObject)GameObject.Instantiate (playerData );
		SystemValues.thePlayer = thePlayer;

		if (SystemValues.theSaveData == null)
			thePlayer.transform.position = theStartPosition.position;
		
		Invoke ("makeStart" , 0.05f);
	}

	void makeStart()
	{
		if (SystemValues.theSaveData != null)
			SystemValues.makeTrueLoad ();
		else if (SystemValues.theDataCatch != null)
			SystemValues.LoadCatch ();
		
		loadGameInformation ();
		loadSetting ();
		ShowStartTalk ();
		Destroy (this);
	}

	/// <summary>
	/// 加载文档信息.
	/// </summary>
	void loadGameInformation()
	{
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

	void ShowStartTalk()
	{
		if (PlotType < 0 || PlotID < 0)
			return;
		
		string plotName =  SystemValues.getPlotName (PlotType , PlotID);
		//如果是读档的话，这个部分无果已经被读到，就可以直接跳过了
		if (string.IsNullOrEmpty (plotName))
			return;

		UIController.GetInstance ().ShowUI<TalkCanvas> (plotName);
	}


}
