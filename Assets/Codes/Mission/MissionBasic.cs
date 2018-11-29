using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]//标记为可序列化的
public class MissionBasic 
{

	//任务类的基类
	//因为任务是可以new出来并且不是脚本，所以不继承mono
	//此外任务类需要一个任务背包，这个任务背包就是需要挂在身上的脚本了
	public string missionName = "";//任务名称
	public string missionInformation = "";//任务说明
	[System.NonSerialized]
	public Player thePlayer;//是哪一个玩家的任务
	public virtual void OnPlayerKill(Player aim){}//击杀任务用这个方法处理
	public virtual void MakeStart(){}//任务信息的设定
	public virtual bool checkMissionOver(){return true;}//每一个任务有自己的任务完成检测方法
	public virtual void OnMissionOver(){}//任务结束会发生什么
}
