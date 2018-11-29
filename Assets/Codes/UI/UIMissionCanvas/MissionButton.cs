using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour {

	//任务界面中选择当前任务的按钮

	private MissionBasic theMission;
	private Text missionNametext;
	private Text missionInformationText;
	private Image theMissionImage;

	public void SetMission(MissionBasic In , Text missionText)
	{
		if (!missionNametext)
			missionNametext = this.GetComponentInChildren<Text> ();
		if (!theMissionImage)
			theMissionImage = this.GetComponentInChildren<Image> ();


		theMission = In;
		missionNametext.text = theMission.missionName;
		missionInformationText = missionText;
		if (In is MainMissionBasic)
			theMissionImage.color = Color.yellow;
		
	}

	//外部选择任务介绍的方法
	public void MakeSelectMission()
   {
		missionInformationText.text = theMission.missionInformation;
   }
}
