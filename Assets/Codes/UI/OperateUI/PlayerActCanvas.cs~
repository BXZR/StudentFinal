using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActCanvas : UIBasic {

	public override void OnEndShow ()
	{
		SystemValues.thePlayer.GetComponent<move> ().stopMoving ();
	}


	/// <summary>
	/// 召唤设定界面.
	/// </summary>
	public void ShowSettings()
	{
		UIController.GetInstance ().ShowUI<SettingCanvas> ();
	}

	/// <summary>
	/// 召唤任务界面
	/// </summary>
	public void ShowMissions()
	{
		UIController.GetInstance ().ShowUI<UIMissionCanvas> ();
	}
}
