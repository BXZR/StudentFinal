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
		UIController.GetInstance ().ShowUI<UISaveLoadSelect> ("Save");
	}

	public void MakeLoad()
	{
		UIController.GetInstance ().ShowUI<UISaveLoadSelect> ("Load");
	}

	public void MakeExitSetting()
	{
		UIController.GetInstance ().CloseUI<SettingCanvas> ();
	}

	public void MakeExitGame()
	{
		UIController.GetInstance ().ShowUI<SelectMessageBox> ("是否离开游戏？");
		UIController.GetInstance ().GetUI<SelectMessageBox> ().theOperate = new MesageOperate (EndGame);
	}

	private void EndGame()
	{
		Application.Quit ();
	}
}
