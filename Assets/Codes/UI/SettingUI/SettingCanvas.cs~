using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCanvas : UIBasic
{

	//系统设定就在这里了
	//系统设定不会多的

	public override void OnShow (string value = "")
	{
		Time.timeScale = 0f;
	}


	public override void OnEndShow ()
	{
		Time.timeScale = 1f;
	}

	public void MakeSave()
	{
		bool saveOp = SystemValues.SaveInformation ();
		string show = saveOp ? "存档成功" : "存档失败";
		UIController.GetInstance ().ShowUI<messageBox> (show);
	}

	public void MakeLoad()
	{
		bool loadOp = SystemValues.LoadInformation ();
		string show = loadOp ? "读取成功" : "读取失败";
		UIController.GetInstance ().ShowUI<messageBox> (show);
	}

	public void MakeExitSetting()
	{
		UIController.GetInstance ().CloseUI<SettingCanvas> ();
	}

	public void MakeExitGame()
	{
		Application.Quit ();
	}
}
