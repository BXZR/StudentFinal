using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]
public class MainMission_4 : MainMissionBasic {

	//这是一个跑图任务，没有任何限制

	public override void MakeStart ()
	{
		missionName = "击败魔物";
		missionInformation = "【主线任务】将这只魔物制服再做调查。";
		CreateBoss ();
	}
		
	private void CreateBoss()
	{
		GameObject boss = GameObject.Instantiate( SystemValues.LoadResources<GameObject>("Boss/Boss1"));
		boss.transform.position = SystemValues.thePlayer.transform.position + SystemValues.thePlayer.transform.forward.normalized * 5f;
		boss.GetComponent<monsterBoss> ().StartBoss ();
		//Debug.Log("create boss");
	}
		
	public override void OnMissionOver ()
	{
		if (!this.thePlayer)
			this.thePlayer = SystemValues.thePlayer.GetComponent<Player> ();

		this.thePlayer.theMissionPackage.theMissions.Remove (this);
		this.thePlayer.OnGetLearningValue (80f);
		UIController.GetInstance ().ShowUI<messageBox> ("任务完成，获得80经验");
	}


	public override bool CanUpdate ()
	{
		return true;
	}
	public override void OnMissionUpdate ()
	{
		OnMissionOver ();
	}

}
