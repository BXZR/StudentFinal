using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_KillMonster1 :MissionBasic {



	private int CountUse = 0;
	private int CountUseMax = 5;

	public override void MakeStart ()
	{
		missionName = "击杀魔物1";
		missionInformation = "击杀5只骷髅魔兵";
	}


	public override void OnPlayerKill (Player aim)
	{
		if (aim.playerName == "骷髅魔兵")
			CountUse++;
	}


	public override bool checkMissionOver ()
	{
		if (CountUse >= CountUseMax)
			return true;
		return false;
	}

	public override void OnMissionOver ()
	{
		this.thePlayer.OnGetLearningValue (50f);
		UIController.GetInstance ().ShowUI<messageBox> ("任务["+missionName+"]已经完成\n获得50经验");
	}
}
