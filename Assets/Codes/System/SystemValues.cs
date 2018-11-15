using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

	 
}
