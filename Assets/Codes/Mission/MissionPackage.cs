using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPackage : MonoBehaviour {

	//挂在人物身上的脚本
	//任务背包
	//这里面容纳任务，并且提供刷新UI的事件

	public  List<MissionBasic> theMissions = new List<MissionBasic> ();
	private Player thePlayer;//这个任务背包的从属
	private MissionBasic theMainMission = null;//这个任务全局唯一的主线任务

	public void AddNewMission(MissionBasic theMission)
	{
		

		//添加任务到背包
		//主线任务只会有一个的
		if (theMission is MainMissionBasic) 
		{
			if (theMainMission != null)
				theMainMission.OnMissionOver ();
			theMissions.RemoveAll (X => X is MainMissionBasic);
			theMainMission = theMission;
			//print ("主线任务更新");
		}
			
		for(int i = 0 ; i< theMissions.Count ; i ++)
		{
			if (theMissions [i].GetType ().Equals(theMission.GetType ())) 
			{
				UIController.GetInstance ().ShowUI<messageBox> ("不可重复获得任务，请完成当前任务之后重试");
				return;
			}
		}
		theMission.thePlayer = this.thePlayer;
		theMissions.Add (theMission);
		UIController.GetInstance ().ShowUI<messageBox> ("更新任务："+theMission.missionName);
	}


	/// <summary>
	/// 自行检查每一个任务的完成情况
	/// </summary>
	private void makeMissionCheck()
	{
		List<MissionBasic> toDelete = new List<MissionBasic> ();
		for (int i = 0; i < theMissions.Count; i++) 
		{
			if (theMissions [i].checkMissionOver ())
				toDelete.Add (theMissions[i]);
		}
		for (int i = 0; i < toDelete.Count; i++) 
		{
			toDelete [i].OnMissionOver ();
			theMissions.Remove (toDelete [i]);
		}
	}


	/// <summary>
	/// 所有的任务在这里统一检测击杀事件
	/// </summary>
	/// <param name="theAim">The aim.</param>
	private void OnKill(Player theAim)
	{
		for (int i = 0; i < theMissions.Count; i++)
			theMissions [i].OnPlayerKill (theAim);
	}

	void Start () 
	{
		this.thePlayer = this.GetComponent<Player> ();
		if(thePlayer)
			thePlayer.KillEvent += OnKill;

		InvokeRepeating ("makeMissionCheck" , 0f , 0.1f);
	}
	

}
