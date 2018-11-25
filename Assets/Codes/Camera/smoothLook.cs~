using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class smoothLook : MonoBehaviour {

	//摄像机的移动
	//这是一个围绕目标球形查看的一个摄像机
	public Transform theTarget;//查看目标
	public float distance = 4f;//与目标的距离
	public float height = 2f;//固定高度

	private Vector3 mousePosition = Vector3.zero;
	private bool canOperate = false;//是否可以操作摄像机

	//完全固定视角摄像机
	public Vector3 extraDistance = new Vector3 (0,2,-2);
	//当前是否是剧情摄像机
	//进入剧情和离开剧情的时候需要修改这个标记
	public bool isPloting = false;

	void Start ()
	{
		canOperate = true;
		FixedPostion ();
		canOperate = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		CameraOperate ();
		else if (Application.platform == RuntimePlatform.Android)
		CameraOperateTouch ();
	}

	void LateUpdate()
	{
		FixedPostion ();
	}


	/// <summary>
	/// 摄像机的触摸操作
	/// 进化版，可以检测多个手指的移动
	/// </summary>
	private void CameraOperateTouch()
	{
		if (!theTarget) 
		{
			theTarget = SystemValues.thePlayer.transform;
			SystemValues.theCamera = this;
			canOperate = true;
			FixedPostion ();
		}
		if (!theTarget)
			return;


		if (SystemValues.IsOperatingUI () && Input.touches.Length >= 2 && !SystemValues.IsOperatingUI(Input.GetTouch (1).fingerId)) 
		{
			canOperate = true;
			MoveWithTouch (Input.GetTouch (1));
		} 
		else if (!SystemValues.IsOperatingUI () && Input.touches.Length >= 1) 
		{
			canOperate = true;
			MoveWithTouch (Input.GetTouch (0));
		} 
		else
		{
			canOperate = false;
		}

	}

	/// <summary>
	/// 封装的touch移动方法
	/// 传入的参数就是Touch
	/// </summary>
	/// <param name="theTouch">The touch.</param>
	private void MoveWithTouch(Touch theTouch)
	{

		float xMove = theTouch.deltaPosition.x; 
		float yMove = theTouch.deltaPosition.y;
		xMove = Mathf.Abs (xMove) > 9f ? xMove * 0.02f : 0f;
		yMove = Mathf.Abs (yMove) > 9f ? yMove * 0.015f : 0f;
		//取反是可以改变操作的感觉
		xMove = -xMove;
		yMove = -yMove;

		height += yMove;
		distance += yMove;
		height = Mathf.Clamp (height, 2f, 6f);
		distance = Mathf.Clamp (distance, 4f, 8f);

		this.transform.Translate (new Vector3 (xMove, 0f, 0f));

	}
		
	/// <summary>
	/// 摄像机的鼠标操作
	/// </summary>
	private void CameraOperate()
	{
		if (!theTarget) 
		{
			theTarget = SystemValues.thePlayer.transform;
			SystemValues.theCamera = this;
			canOperate = true;
			FixedPostion ();
			canOperate = false;
		}
		if (!theTarget)
			return;
		

		if (Input.GetMouseButtonDown (0)) 
		{
			if (SystemValues.IsOperatingUI ())
				canOperate = false;
			else 
			{
				mousePosition = Input.mousePosition;
				canOperate = true;
			}
		}
		if (Input.GetMouseButton (0)) 
		{
			if (canOperate )
			{
				float xMove = (Input.mousePosition.x - mousePosition.x); 
				float yMove = (Input.mousePosition.y - mousePosition.y);
				xMove = Mathf.Abs (xMove) > 9f ? xMove * 0.02f : 0f;
				yMove = Mathf.Abs (yMove) > 9f ? yMove * 0.015f : 0f;
				//取反是可以改变操作的感觉
				xMove = -xMove;
				yMove = -yMove;
				mousePosition = Input.mousePosition;

				height += yMove;
				distance += yMove;
				height = Mathf.Clamp (height , 2f, 6f);
				distance = Mathf.Clamp (distance  , 4f , 8f);

				this.transform.Translate (new Vector3 (xMove, 0f, 0f));

			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			canOperate = false;
		}
	}


	/// <summary>
	/// 战斗/移动模式之下的摄像机操作
	/// </summary>
	private void MoveCamera()
	{
		if (SystemValues.theCameraState == CameraState.rotateCamera || canOperate) 
		{
			Vector3 aimPosition = theTarget.transform.position + extraDistance;
			aimPosition = new Vector3 (this.transform.position.x, theTarget.transform.position.y + height, this.transform.position.z);
			extraDistance = (aimPosition - theTarget.transform.position).normalized * distance;
			this.transform.LookAt (theTarget.transform.position + new Vector3 (0f, 1f, 0f));
			this.transform.position = theTarget.transform.position + extraDistance;
			//不能用插值，会有动荡
			//this.transform.position = Vector3.Lerp(this.transform.position ,  theTarget.transform.position + extraDistance , 0.8f);
		} 
		else if (SystemValues.theCameraState == CameraState.fixedCamera) 
		{
			this.transform.position = theTarget.transform.position + extraDistance;
			//不能用插值，会有动荡
			//this.transform.position = Vector3.Lerp(this.transform.position ,  theTarget.transform.position + extraDistance , 0.5f);
		}
	}

	/// <summary>
	/// 剧情模式之下的摄像机操作
	/// </summary>
	private void PlotCamera()
	{
		if (!theTarget)
			return;
		
		this.transform.position = theTarget.transform.position + theTarget.transform.rotation * new Vector3 (2f,2f,2f) ;
		this.transform.LookAt (theTarget.transform.position  + theTarget.transform.rotation *  new Vector3(0f,1.5f,1f) );
	}

	/// <summary>
	/// 在进入剧情的时候可以发生的事情
	/// </summary>
	public void OnIntoPlot()
	{
		isPloting = true;
	}


	/// <summary>
	/// 离开剧情模式下的额外操作
	/// </summary>
	public void OnOutPlot()
	{
		isPloting = false;
		this.transform.position = theTarget.transform.position + extraDistance;
		this.transform.LookAt (theTarget.transform.position + new Vector3 (0f, 1f, 0f));

	}

	/// <summary>
	/// 摄像机的青位置修订操作
	/// </summary>
	private void FixedPostion()
	{
		if (!theTarget)
			return;

		if (!isPloting)
			MoveCamera ();
		else
			PlotCamera ();

	}
		
}
	
