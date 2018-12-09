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
			//这个任务可以更新
			//主线任务都是可以更新的，一些boss战的任务是靠更新完结的
			if (missionHave.CanUpdate ()) 
			{
				missionHave.OnMissionUpdate ();
				print ("update mission");
			}
			else 
			{
				//如果这个任务可以重复领取
				if (missionHave.checkMissionOver ()) 
				{
					missionHave.OnMissionOver ();
					theMissions.Remove (missionHave);
					theMission.thePlayer = this.thePlayer;
					theMissions.Add (theMission);
				}
			}
		}
		else
		{
			theMission.thePlayer = this.thePlayer;
			theMissions.Add (theMission);
			UIController.GetInstance ().ShowUI<messageBox> ("获得新任务");
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


	/// <summary>
	/// 交互事件在这里处理
	/// </summary>
	/// <param name="aim">Aim.</param>
	private void OnInteractive(InteractiveBasic aim)
	{
		theMissions.RemoveAll (X => X==null);
		for (int i = 0; i < theMissions.Count; i++)
			theMissions [i].OnPlayerIntweactive (aim);
	}



	void Start () 
	{
		this.thePlayer = this.GetComponent<Player> ();
		if (thePlayer)
		{
			thePlayer.KillEvent += OnKill;
			thePlayer.InteractiveEvent += OnInteractive;
		}
	}
	

}
