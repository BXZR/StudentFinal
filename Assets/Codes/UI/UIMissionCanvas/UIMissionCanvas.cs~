using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionCanvas : UIBasic {

	//游戏任务界面
	//左边一堆按钮选择任务
	//右边就是一个Text介绍任务
	//间直接是超级水的山寨任务界面了

	public Transform MissionButtonfather;//任务按钮挂在这个下面
	public GameObject MissionButtonProfab;//任务按钮的预设物
	public Text missionInformationText;//任务信息介绍text
	private List<GameObject> theMissionButtons = new List<GameObject> ();


	public override void OnEndShow ()
	{
		Time.timeScale = 1f;
		this.gameObject.SetActive (false);
	}

	public override void OnShow (string value = "")
	{
		Time.timeScale = 0f;
		missionInformationText.text = "尚未选择任务";
		for (int i = 0; i < theMissionButtons.Count; i++)
			Destroy (theMissionButtons[i].gameObject);

		MissionPackage thePackage = SystemValues.thePlayer.GetComponent<Player> ().theMissionPackage;
		for (int i = 0; i < thePackage.theMissions.Count; i++)
		{
			GameObject aMission = (GameObject)GameObject.Instantiate (MissionButtonProfab);
			aMission.transform.SetParent (MissionButtonfather);
			aMission.GetComponent<MissionButton> ().SetMission ( thePackage.theMissions[i], missionInformationText);
			theMissionButtons.Add (aMission);
		}
	} 

	 
}
