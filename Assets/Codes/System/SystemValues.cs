﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Xml;
using UnityEngine.SceneManagement;

public enum CameraState{fixedCamera ,rotateCamera}
/// <summary>
///这个类存放的是公有方法和静态参数
/// </summary>
public class SystemValues : MonoBehaviour {

	//为了保证设定面板的简洁，暂时隐藏的一些参数
	public static string xAxisName = "Vertical";//左右移动的轴名称
	public static string yAxisName = "Horizontal";//前后移动的轴名称
	public static CameraState theCameraState = CameraState.fixedCamera;//摄像机模式
	public static GameObject thePlayer = null;//游戏玩家


	/// <summary>
	/// 修改摄像机的操作方式
	/// </summary>
	public static void ChangeCameraMode()
	{
		if (theCameraState == CameraState.fixedCamera)
			theCameraState = CameraState.rotateCamera;
		else
			theCameraState = CameraState.fixedCamera;
	}

	/// <summary>
	/// 是否正在操作UI，适用于UGUI
	/// </summary>
	public static bool IsOperatingUI()
	{
		if (EventSystem.current == null)
			return false;
		
		//目前也就支持PC和按照两种平台的简单交互了
		if(Application .platform == RuntimePlatform.Android)
		{
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
				return true;//print  ("当前触摸在UI上");
			else 
				return false;//print  ("当前没有触摸在UI上");
		}
		else
		{
			//这是两个平台的不同判断方法
			if (EventSystem.current.IsPointerOverGameObject ())
				return true;//print  ("当前触摸在UI上");
			else 
				return false;//print  ("当前没有触摸在UI上");
		}
	}

	/// <summary>
	/// 获取输入操作
	/// </summary>
	public static  Vector2 getAxisInputPC()
	{
		float xAxisValue = Input.GetAxis (yAxisName);
		float yAxisValue= Input.GetAxis (xAxisName);
		xAxisValue= Mathf.Abs (xAxisValue) < 0.6f ? 0f : xAxisValue;
		yAxisValue = Mathf.Abs (yAxisValue) < 0.6f ? 0f : yAxisValue;
		return new Vector2 (xAxisValue , yAxisValue);
	}

	#region 有关剧本操作 
	//显示对话
	private static XmlDocument xml = new XmlDocument();
	private static XmlNodeList theXmlList;
	private static List<PlotItem>theFrames = new List<PlotItem> ();
	//当前的剧本ID，第一项为主线剧本ID，之后的都是支线，如果有5个支线就是后面5个ID
	//这个是当前存档的时候需要重点存储的内容之一
	//记录ID，这样很多的东西都不会重复进行了
	private static int[] plotIDNow = {0,0,0,0,0,0 };

	public static void LoadPlots()
	{
		theFrames = new List<PlotItem> ();
		TextAsset textAsset = (TextAsset)Resources.Load ("XML/" + "Plot");
		xml.LoadXml (textAsset.text);
		theXmlList = xml.SelectNodes ("Root/Item");
		foreach (XmlNode node in theXmlList) 
		{
			PlotItem aFrame = new PlotItem();
			aFrame.ID = XmlConvert.ToInt32( node.SelectSingleNode("ID").InnerText);
			aFrame.Plot = node.SelectSingleNode ("Plot").InnerText;
			aFrame.Name = node.SelectSingleNode ("Name").InnerText;
			aFrame.type = XmlConvert.ToInt32(node.SelectSingleNode ("Type").InnerText);
			aFrame.Information = node.SelectSingleNode ("Information").InnerText;
			theFrames.Add (aFrame);
		}
	}

	/// <summary>
	/// 通过剧本ID获取到当前剧本文件的名字
	/// 参数plotType为需要调用的剧本类型ID
	/// </summary>
	public static string getPlotName(int plotType)
	{
		if(plotType >= plotIDNow.Length)
			return "";
		
		int IDUse = plotIDNow [plotType];
		//循环查找，此处可以优化
		string plotNameUse = "";
		for (int i = 0; i < theFrames.Count; i++) 
		{
			if (theFrames [i].type == plotType && theFrames [i].ID == IDUse) 
			{
				plotNameUse = theFrames [i].Plot;
				plotIDNow [plotType]++;
			}
		}

		return plotNameUse;
	}
	#endregion

	#region 文件操作
	//加载的存档信息
	public static GameData theSaveData = null;

	public static bool SaveInformation()
	{
		//存档校验
		if (!SystemValues.thePlayer)
			return false;

		Player thePlayers = SystemValues.thePlayer.GetComponent<Player> ();
		if (!thePlayers)
			return false;

		//构建存档信息
		GameData theData = new GameData ();
		theData.playerLv = thePlayers.lvNow;
		theData.playerDamge = thePlayers.attackDamage;
		theData.playerHp = thePlayers.hpNow;
		theData.playerHpMax = thePlayers.hpMaxNow;
		theData.playerLearn = thePlayers.learningValue;
		theData.playerLearnMax = thePlayers.learningValueMax;
		theData.plotIDs = SystemValues.plotIDNow;
		theData.SceneName = Application.loadedLevelName;
		theData.playerPositionX = thePlayers.transform.position.x;
		theData.playerPositionY = thePlayers.transform.position.y;
		theData.playerPositionZ = thePlayers.transform.position.z;

		//真实存档
		FileOperater fileOp = new FileOperater ();
		string fileName = Application.persistentDataPath + "/GameData.sav";
		print (fileName);
		fileOp.SaveBinary (fileName , theData);
		return true;
	}


	public static bool LoadInformation()
	{
		//读取信息
		FileOperater fileOp = new FileOperater ();
		string fileName = Application.persistentDataPath + "/GameData.sav";
		GameData theData = fileOp.loadBinary (fileName);

		//存档校验
		if (theData == null || SystemValues.thePlayer == null)
			return false;

		SystemValues.theSaveData = theData;
		UIController.GetInstance ().ShowUI<UILoading> (theData.SceneName);
		return true;

	}


	public static void makeTrueLoad()
	{
		Player thePlayers = SystemValues.thePlayer.GetComponent<Player> ();
		if (!thePlayers || SystemValues.theSaveData == null)
			return;

		thePlayers.lvNow = SystemValues.theSaveData.playerLv;
		thePlayers.attackDamage = SystemValues.theSaveData.playerDamge;
		thePlayers.hpNow = SystemValues.theSaveData.playerHp;
		thePlayers.hpMaxNow = SystemValues.theSaveData.playerHpMax;
		thePlayers.learningValue = SystemValues.theSaveData.playerLearn;
		thePlayers.learningValueMax = SystemValues.theSaveData.playerLearnMax;
		SystemValues.plotIDNow = SystemValues.theSaveData.plotIDs;
		thePlayer.transform.position = new Vector3 (SystemValues.theSaveData.playerPositionX , SystemValues.theSaveData.playerPositionY , SystemValues.theSaveData.playerPositionZ);

		SystemValues.theSaveData = null;

	}
	#endregion
}

//剧本信息存储类
class PlotItem
{
	public int ID = 0;
	public int type = 0;
	public string Plot = "";
	public string Name = "";  
	public string Information = "";
}
