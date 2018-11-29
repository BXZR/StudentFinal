using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMission_1 : MainMissionBasic {

	//这是一个跑图任务，没有任何限制
	public override void MakeStart ()
	{
		missionName = "城中查探";
		missionInformation = "【主线任务】城中似乎出现了妖兽的影子，为了防止城中清平受到破坏，在城中四处查探一番。";
	}

	public override void OnMissionOver ()
	{
		this.thePlayer.OnGetLearningValue (25f);
		UIController.GetInstance ().ShowUI<messageBox> ("主线任务["+missionName+"]已经完成\n获得25经验");
	}

	public override bool checkMissionOver ()
	{
		return false;
	}
}
