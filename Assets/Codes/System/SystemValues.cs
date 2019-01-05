using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Xml;
using UnityEngine.SceneManagement;

public enum CameraState{fixedCamera , rotateCamera , PlotCamera}
/// <summary>
///这个类存放的是公有方法和静态参数
/// </summary>
public class SystemValues : MonoBehaviour {

	//为了保证设定面板的简洁，暂时隐藏的一些参数
	public static string xAxisName = "Vertical";//左右移动的轴名称
	public static string yAxisName = "Horizontal";//前后移动的轴名称
	public static CameraState theCameraState = CameraState.fixedCamera;//摄像机模式
	public static GameObject thePlayer = null;//游戏玩家
	public static smoothLook theCamera = null;//游戏摄像机
	public static bool isNewGame = false;//是否是一个全新的游戏

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
	/// 可以传入参数，参数为Input.GetTouch的fingerId
	/// 默认参数为Input.GetTouch(0).fingerId
	/// </summary>
	public static bool IsOperatingUI( int IDIn = -1)
	{
		if (EventSystem.current == null)
			return false;

	
		//目前也就支持PC和按照两种平台的简单交互了
		if(Application .platform == RuntimePlatform.Android)
		{
			int IDUse = IDIn >= 0 ? IDIn : Input.GetTouch(0).fingerId; 
			if (EventSystem.current.IsPointerOverGameObject(IDUse)) 
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


	public static bool loadWithResouces = true;
//	public static GameObject LoadResources(string path) 
//	{
//		if (loadWithResouces)
//			return Resources.Load<GameObject>(path);
//		return null;
//	}

	public static T LoadResources<T>(string path) where T : Object
	{
		if (loadWithResouces)
			return Resources.Load<T>(path);
		return null;
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
	public static int getPlotID(int type = 0 )
	{
		if (type >= plotIDNow.Length)
			return  -1;
		return plotIDNow [type];
	}


	/// <summary>
	/// 通过剧本ID获取到当前剧本文件的名字
	/// 参数plotType为需要调用的剧本类型ID
	/// 这种做法非常讲究命名规范，剧本是否能够查找到都要考剧本命名
	/// 一个剧本Type一个文件夹，文件夹下的文件命名为“Plot_Type_ID”
	/// </summary>
	public static string getPlotName(int plotType , int PlotID)
	{
		if(plotType >= plotIDNow.Length)
			return "";
		
		int IDUse = plotIDNow [plotType];

		string plotNameUse = "";

		if(IDUse == PlotID)
		{
		 plotNameUse = "PlotItem" +  plotType + "/Plot_"+plotType+"_" + IDUse;
		 plotIDNow [plotType]++;
		}

		return plotNameUse;
	}
		
	#endregion

	#region 文件操作
	//加载的存档信息
	public static GameData theSaveData = null;
	public static bool SaveInformation(string ID = "0")
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

		theData.missions = thePlayers.theMissionPackage.theMissions;
		
		//真实存档
		FileOperater fileOp = new FileOperater ();
		string fileName = Application.persistentDataPath + "/GameData"+ID+".sav";
		print (fileName);
		fileOp.SaveBinary (fileName , theData);
		return true;
	}


	public static bool LoadInformation(string ID = "0")
	{
		//读取信息
		FileOperater fileOp = new FileOperater ();
		string fileName = Application.persistentDataPath + "/GameData"+ID+".sav";
		GameData theData = fileOp.loadBinary (fileName);

		//存档校验
		if (theData == null)
			return false;

		SystemValues.theSaveData = theData;
		UIController.GetInstance ().ShowUI<UILoading> (theData.SceneName);
		return true;

	}

	//获取存档信息
	public static GameData getData(string ID = "0")
	{
		//读取信息
		FileOperater fileOp = new FileOperater ();
		string fileName = Application.persistentDataPath + "/GameData"+ID+".sav";
		GameData theData = fileOp.loadBinary (fileName);
		return theData;
	}

	//完全从头来过
	public static void MakeAllStartFlash()
	{
		plotIDNow = new  int[] {0,0,0,0,0,0 };
	}

	public static void makeTrueLoad()
	{
		Player thePlayers = SystemValues.thePlayer.GetComponent<Player> ();
		if (!thePlayers || SystemValues.theSaveData == null)
			return;

		//加载数据
		thePlayers.lvNow = SystemValues.theSaveData.playerLv;
		thePlayers.attackDamage = SystemValues.theSaveData.playerDamge;
		thePlayers.hpNow = SystemValues.theSaveData.playerHp;
		thePlayers.hpMaxNow = SystemValues.theSaveData.playerHpMax;
		thePlayers.learningValue = SystemValues.theSaveData.playerLearn;
		thePlayers.learningValueMax = SystemValues.theSaveData.playerLearnMax;
		SystemValues.plotIDNow = SystemValues.theSaveData.plotIDs;

		for (int i = 0; i < SystemValues.theSaveData.missions.Count; i++)
			thePlayers.theMissionPackage.AddNewMission (SystemValues.theSaveData.missions [i]);

		thePlayer.transform.position = new Vector3 (SystemValues.theSaveData.playerPositionX , SystemValues.theSaveData.playerPositionY , SystemValues.theSaveData.playerPositionZ);
		//刷新一下数值，更改其他显示
		thePlayers.MakeFlash();
		SystemValues.theSaveData = null;

	}
	#endregion

	#region 信息缓存
	//加载的catch
	//有些跨场景的内容是存放在这里的
	public static GameData theDataCatch = null;
	public static void  SaveCatch()
	{
		//存档校验
		if (!SystemValues.thePlayer)
			return ;

		Player thePlayers = SystemValues.thePlayer.GetComponent<Player> ();
		if (!thePlayers)
			return ;

		//构建存档信息
		theDataCatch = new GameData ();
		theDataCatch.playerLv = thePlayers.lvNow;
		theDataCatch.playerDamge = thePlayers.attackDamage;
		theDataCatch.playerHp = thePlayers.hpNow;
		theDataCatch.playerHpMax = thePlayers.hpMaxNow;
		theDataCatch.playerLearn = thePlayers.learningValue;
		theDataCatch.playerLearnMax = thePlayers.learningValueMax;
		theDataCatch.plotIDs = SystemValues.plotIDNow;
		theDataCatch.missions = thePlayers.theMissionPackage.theMissions;
	}

	public static void LoadCatch()
	{
		Player thePlayers = SystemValues.thePlayer.GetComponent<Player> ();
		if (!thePlayers || SystemValues.theDataCatch== null)
			return;

		//加载数据
		thePlayers.lvNow = SystemValues.theDataCatch.playerLv;
		thePlayers.attackDamage = SystemValues.theDataCatch.playerDamge;
		thePlayers.hpNow = SystemValues.theDataCatch.playerHp;
		thePlayers.hpMaxNow = SystemValues.theDataCatch.playerHpMax;
		thePlayers.learningValue = SystemValues.theDataCatch.playerLearn;
		thePlayers.learningValueMax = SystemValues.theDataCatch.playerLearnMax;
		SystemValues.plotIDNow = SystemValues.theDataCatch.plotIDs;

		for (int i = 0; i < SystemValues.theDataCatch.missions.Count; i++)
			thePlayers.theMissionPackage.AddNewMission (SystemValues.theDataCatch.missions [i]);

		//刷新一下数值，更改其他显示
		thePlayers.MakeFlash();
		SystemValues.theDataCatch = null;

	}
	#endregion


	#region 头顶血条管理器
	public static List<PlayerBloodCanvas> bloodCamvasList = new List<PlayerBloodCanvas> ();

	public static void ShowBloodCanvas()
	{
		bloodCamvasList.RemoveAll (X => X == null);
		for (int i = 0; i < bloodCamvasList.Count; i++)
			bloodCamvasList [i].gameObject.SetActive (true);
	}


	public static void CloseBloodCanvas()
	{
		bloodCamvasList.RemoveAll (X => X == null);
		for (int i = 0; i < bloodCamvasList.Count; i++)
			bloodCamvasList [i].gameObject.SetActive (false);
	}

	#endregion

	#region寻找目标
	//角度转弧度的方法
	private static float change(float angle)
	{
		return( angle * Mathf.PI / 180);
	}

	public static List<GameObject> theEMY = new List<GameObject> ();
	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	public static List<GameObject> searchAIMs(float angle , float distance ,Transform theSearcherTransform)//不使用射线而是使用向量计算方法
	{
		//这个方法的正方向使用的是X轴正方向
		//具体使用的时候非常需要注意正方向的朝向
		theEMY.Clear();
		//以自己为中心进行相交球体探测
		//实际上身边一定圆周范围内的所有具有碰撞体的单位都会被被这一步探测到
		//接下来需要的就是对坐标进行审查
		Collider [] emys = Physics.OverlapSphere (theSearcherTransform.position, distance);
		//使用cos值进行比照，因为在0-180角度范围内，cos是不断下降的
		//具体思路就是，判断探测到的物体的cos值如果这个cos值大于标准值，就认为这个单位的角度在侦查范围角度内。
		float angleCosValue = Mathf.Cos (change(angle));//莫认真侧角度的cos值作为计算标准
		//print ("angleCosValue-"+angleCosValue);
		for (int i = 0; i < emys.Length; i++)//开始对相交球体探测物体进行排查
		{ 
			//用alive标记减少在这里参与计算的单位数量
			if (emys [i].gameObject != theSearcherTransform.gameObject) //相交球最大的问题就是如果自身有碰撞体，自己也会被侦测到
			{
				//print ("name-"+ emys [i].name);
				Vector3 thisToEmy = emys [i].transform.position - theSearcherTransform.transform.position;//目标坐标减去自身坐标
				Vector2 theVectorToSearch = (new Vector2 (thisToEmy.x, thisToEmy.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				Vector2 theVectorForward = (new Vector2 (theSearcherTransform.transform.forward.x, theSearcherTransform.transform.forward.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				float cosValue = (theVectorForward.x * theVectorToSearch.x + theVectorForward.y * theVectorToSearch.y);//因为已经单位化，就没必要再进行求模计算了
				//print ("cosValue-" + cosValue);
				/*
				    先求出两个向量的模
					再求出两个向量的向量积
					|a|=√[x1^2+y1^2]
					|b|=√[x2^2+y2^2]
					a*b=(x1,y1)(x2,y2)=x1x2+y1y2
					cos=a*b/[|a|*|b|]
					=(x1x2+y1y2)/[√[x1^2+y1^2]*√[x2^2+y2^2]]
				*/
				if (cosValue >= angleCosValue)//如果cos值大于基准值，认为这个就是应该被探测的目标
				{
					if (theEMY .Contains (emys [i].gameObject) == false) //不重复地放到已找到的列表里面
					{
						theEMY.Add (emys [i].gameObject);
						//print ("SeachFind "+emys [i].GetComponent<Collider> ().gameObject.name);//找到目标
					}
				}
			}
		}
		return theEMY;
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
