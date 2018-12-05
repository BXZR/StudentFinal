using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]
public class MainMission_1 : MainMissionBasic {


	public int index = 0;

	public string [] missionNames = { "城中查探", "城中法阵", "探视李家" , "凤血草" };
	public string[] informationData = 
	{
		"【主线任务】城中似乎出现了妖兽的影子，为了防止城中清平受到破坏，在城中四处查探一番。",
		"【主线任务】城后方有似乎是这个神秘法阵的源头，过去查看一下。",
		"【主线任务】去李家探视小李子的情况，或许会有所发现",
		"【主线任务】前去城外寻找凤血草"
	};
	//这是一个跑图任务，没有任何限制
	public override void MakeStart ()
	{
		missionName = "城中查探";
		missionInformation = "【主线任务】城中似乎出现了妖兽的影子，为了防止城中清平受到破坏，在城中四处查探一番。";

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
