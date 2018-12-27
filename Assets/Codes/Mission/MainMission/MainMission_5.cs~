using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]
public class MainMission_5 : MainMissionBasic {


	public int index = 0;

	public string [] missionNames = { "李生情况"};
	public string[] informationData = 
	{
		"【主线任务】李生的情况到底是怎么回事，还需要继续探查才能明白。"
	};
	//这是一个跑图任务，没有任何限制
	public override void MakeStart ()
	{
		missionName = missionNames [index];
		missionInformation = informationData [index];

	}


	/// <summary>
	/// 这个主线任务是可以更新的
	/// </summary>
	/// <returns><c>true</c> if this instance can update; otherwise, <c>false</c>.</returns>
	public override bool CanUpdate ()
	{
		return true;
	}

	/// <summary>
	///任务更新换名字
	/// </summary>
	public override void OnMissionUpdate ()
	{
		index++;
		if (checkMissionOver ())
			OnMissionOver ();
		else 
		{
			UIController.GetInstance ().ShowUI<messageBox> ("任务更新");
			this.thePlayer.OnGetLearningValue (20f);
			missionName = missionNames [index];
			missionInformation = informationData [index];
		}
	}


	public override void OnMissionOver ()
	{
		if (!this.thePlayer)
			this.thePlayer = SystemValues.thePlayer.GetComponent<Player> ();

		this.thePlayer.theMissionPackage.theMissions.Remove (this);
		this.thePlayer.OnGetLearningValue (80f);
		UIController.GetInstance ().ShowUI<messageBox> ("任务完成，获得80经验");
	}


	public override bool checkMissionOver ()
	{
		return index >= missionNames.Length;
	}


}
