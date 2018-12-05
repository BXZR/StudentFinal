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
		theMissions.RemoveAll(X=>X==null);
		MissionBasic missionHave = theMissions.Find (X =>X.GetType ().Equals(theMission.GetType ()));
		if (missionHave != null ) 
		{
			if (missionHave.CanUpdate ())
				missionHave.OnMissionUpdate ();
			else 
			{
				missionHave.OnMissionOver ();
				theMissions.Remove (missionHave);
				theMission.thePlayer = this.thePlayer;
				theMissions.Add (theMission);
			}
		}
		else
		{
			theMission.thePlayer = this.thePlayer;
			theMissions.Add (theMission);
		}

	}


	/// <summary>
	/// 所有的任务在这里统一检测击杀事件
	/// </summary>
	/// <param name="theAim">The aim.</param>
	private void OnKill(Acter theAim)
	{
		for (int i = 0; i < theMissions.Count; i++)
			theMissions [i].OnPlayerKill (theAim);
	}


	void Start () 
	{
		this.thePlayer = this.GetComponent<Player> ();
		if(thePlayer)
			thePlayer.KillEvent += OnKill;
	}
	

}
