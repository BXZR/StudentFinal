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
	private List<MissionButton> theMissionButtons = new List<MissionButton> ();


	private Queue<MissionButton> missionButtonsPool = new Queue<MissionButton> ();

	public override void OnEndShow ()
	{
		Time.timeScale = 1f;
		this.gameObject.SetActive (false);
	}

	public override void OnShow (string value = "")
	{
		Time.timeScale = 0f;
		ShowButtonsNew ();
		if(theMissionButtons.Count > 0 )
			 theMissionButtons[0].MakeSelectMission();
		else
		    missionInformationText.text = "当前没有任务";
	} 


	//旧版显示按钮的方法
	private void ShowBttonsOld()
	{
		for (int i = 0; i < theMissionButtons.Count; i++)
			Destroy (theMissionButtons[i].gameObject);
		theMissionButtons.Clear ();

		MissionPackage thePackage = SystemValues.thePlayer.GetComponent<Player> ().theMissionPackage;
		for (int i = 0; i < thePackage.theMissions.Count; i++)
		{
			MissionButton aMission = GameObject.Instantiate (MissionButtonProfab).GetComponent<MissionButton>();
			aMission.transform.SetParent (MissionButtonfather);
			aMission.SetMission ( thePackage.theMissions[i], missionInformationText);
			theMissionButtons.Add (aMission);
		}
	}


	//新版显示按钮的方法
	private void ShowButtonsNew()
	{
		for (int i = 0; i < theMissionButtons.Count; i++) 
		{
			missionButtonsPool.Enqueue (theMissionButtons[i]);
			theMissionButtons [i].gameObject.SetActive (false);
		}
		theMissionButtons.Clear ();

		MissionPackage thePackage = SystemValues.thePlayer.GetComponent<Player> ().theMissionPackage;
		for (int i = 0; i < thePackage.theMissions.Count; i++)
		{
			MissionButton aMission;
			if (missionButtonsPool.Count > 0) 
			{
				aMission = missionButtonsPool.Dequeue ();
				aMission.gameObject.SetActive (true);
			}
			else
				aMission = GameObject.Instantiate (MissionButtonProfab).GetComponent<MissionButton>();
			
		
			aMission.SetMission ( thePackage.theMissions[i], missionInformationText);
			theMissionButtons.Add (aMission);
		}
		theMissionButtons.Sort ((x, y) => ( x.missionID .CompareTo( y.missionID)) );
		//先完成排序再加入
		for(int i = 0 ; i < theMissionButtons.Count ; i ++)
			theMissionButtons[i].transform.SetParent (MissionButtonfather);

	}

	 
}
