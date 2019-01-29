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
	public int missionID = 0;//用于排序的ID
	private ColorBlock colorBuff;//用来存储本来的颜色

	public void SetMission(MissionBasic In , Text missionText)
	{
		if (!missionNametext)
			missionNametext = this.GetComponentInChildren<Text> ();
		if (!theMissionButton)
			theMissionButton = this.GetComponentInChildren<Button> ();


		theMission = In;
		missionNametext.text = theMission.missionName;
		missionInformationText = missionText;
		missionID = 1;

		if (In is MainMissionBasic)
		{
			ColorBlock mainMissionColor = new ColorBlock ();
			mainMissionColor.normalColor = Color.magenta;
			mainMissionColor.highlightedColor = Color.magenta;
			mainMissionColor.pressedColor = Color.gray;
			mainMissionColor.colorMultiplier = 1f;
			theMissionButton.colors = mainMissionColor;
			missionID = 0;
		}
		colorBuff = theMissionButton.colors;
		
	}

	//外部选择任务介绍的方法
	public void MakeSelectMission()
   {
		missionInformationText.text = theMission.missionInformation;

		if (selectedMissionButton)
			selectedMissionButton.theMissionButton.colors = selectedMissionButton.colorBuff;
		selectedMissionButton = this;
		this.theMissionButton.colors = GetSelectedMissionColor();
   }
	 

	//有关颜色的额外操作
	private static ColorBlock selectedMissionColor;
	private static MissionButton selectedMissionButton;
	public static ColorBlock GetSelectedMissionColor()
	{
		selectedMissionColor = new ColorBlock ();
		selectedMissionColor.normalColor = Color.cyan;
		selectedMissionColor.highlightedColor = Color.cyan;
		selectedMissionColor.pressedColor = Color.gray;
		selectedMissionColor.colorMultiplier = 1f;
		return selectedMissionColor;
	}
		
}
