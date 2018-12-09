using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour {

	//任务界面中选择当前任务的按钮

	private MissionBasic theMission;
	private Text missionNametext;
	private Text missionInformationText;
	private Button theMissionButton;

	public void SetMission(MissionBasic In , Text missionText)
	{
		if (!missionNametext)
			missionNametext = this.GetComponentInChildren<Text> ();
		if (!theMissionButton)
			theMissionButton = this.GetComponentInChildren<Button> ();


		theMission = In;
		missionNametext.text = theMission.missionName;
		missionInformationText = missionText;

		if (In is MainMissionBasic)
		{
			ColorBlock mainMissionColor = new ColorBlock ();
			mainMissionColor.normalColor = Color.magenta;
			mainMissionColor.highlightedColor = Color.magenta;
			mainMissionColor.pressedColor = Color.gray;
			mainMissionColor.colorMultiplier = 1f;
			theMissionButton.colors = mainMissionColor;
		}
		
	}

	//外部选择任务介绍的方法
	public void MakeSelectMission()
   {
		missionInformationText.text = theMission.missionInformation;
   }
}
