using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]
public class Mission_KillMonster1 :MissionBasic {

	private int CountUse = 0;
	private int CountUseMax = 5;

	public override void MakeStart ()
	{
		missionName = "击杀魔物1";
		missionInformation = "骷髅魔兵竟然出现在这里，实在蹊跷。先击溃这些魔兵再做查看。此任务需击杀5只骷髅魔兵方可完成。";
	}


	public override void OnPlayerKill (Acter aim)
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
		if (!this.thePlayer)
			this.thePlayer = SystemValues.thePlayer.GetComponent<Player> ();
		
		this.thePlayer.OnGetLearningValue (30f);
		UIController.GetInstance ().ShowUI<messageBox> ("任务["+missionName+"]已经完成\n获得30经验");
	}
}
