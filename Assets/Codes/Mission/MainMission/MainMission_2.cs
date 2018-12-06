using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]
public class MainMission_2 : MainMissionBasic {

	int getCount= 0;
	int getMax = 3;
	//这是一个跑图任务，没有任何限制
	public override void MakeStart ()
	{
		missionName = "寻找凤血草";
		missionInformation = "【主线任务】在这凤栖野中有一些凤血草，摘取三株作为救助李生的药引。\n已经采摘"+getCount+"/"+getMax;

	}
		
	public override void OnPlayerIntweactive (InteractiveBasic aim)
	{
		if (aim.InterName != "凤血草")
			return;

		GameObject.Destroy (aim.gameObject);
		getCount++;
		if (checkMissionOver ())
			OnMissionOver ();
		else 
		{
			UIController.GetInstance ().ShowUI<messageBox> ("获得凤血草");
			this.thePlayer.OnGetLearningValue (20f);
			missionInformation = "【主线任务】在这凤栖野中有一些凤血草，摘取三株作为救助李生的药引。\n已经采摘"+getCount+"/"+getMax;
		}
	}


	public override void OnMissionOver ()
	{
		if (!this.thePlayer)
			this.thePlayer = SystemValues.thePlayer.GetComponent<Player> ();

		this.thePlayer.theMissionPackage.theMissions.Remove (this);
		this.thePlayer.OnGetLearningValue (80f);
		UIController.GetInstance ().ShowUI<messageBox> ("任务完成，获得80经验");


		string plotName = SystemValues.getPlotName (0 , 5);
		if (string.IsNullOrEmpty (plotName))
			return;
		UIController.GetInstance ().ShowUI<TalkCanvas> (plotName);
	}


	public override bool checkMissionOver ()
	{
		return getCount >=  getMax;
	}
}
