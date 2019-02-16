using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class smoothLook : MonoBehaviour {

	//摄像机的移动
	//这是一个围绕目标球形查看的一个摄像机
	public Transform theTarget;//查看目标
	//摄像机的高度和距离设置参数
	private float maxHeight = 6f;
	private float minHeight = 2f;
	private float maxDistance = 8f;
	private float minDistance = 4f;
	private float maxFov = 60f;
	private float minFov = 30f;
	//摄像机本体
	private Camera theCamera;

	public float distance = 4f;//与目标的距离
	public float height = 2f;//固定高度
	private Vector3 mousePosition = Vector3.zero;
	private bool canOperate = false;//是否可以操作摄像机

	//完全固定视角摄像机
	public Vector3 extraDistance = new Vector3 (0,6,-5);
	//当前是否是剧情摄像机
	//进入剧情和离开剧情的时候需要修改这个标记
	public bool isPloting = false;

	void Start ()
	{
		SystemValues.theCamera = this;
		theCamera = this.GetComponent<Camera> ();
		Invoke ("FixPositionOnStart" , 0.02f);
	}

	//开始阶段的强制设定摄像机位置
	void FixPositionOnStart()
	{
		if (!SystemValues.thePlayer )
			return;

		theTarget = SystemValues.thePlayer.transform;

		this.transform.position = theTarget.transform.position + extraDistance;
		this.transform.LookAt(theTarget.transform.position);
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
		
//----------------------------------------------------操作方法封装，获得输入------------------------------------------------------------------------------//
	/// <summary>
	/// 摄像机的触摸操作
	/// 进化版，可以检测多个手指的移动
	/// </summary>
	private void CameraOperateTouch()
	{
		if (!theTarget)
			return;

		if (!SystemValues.IsOperatingUI () && Input.touches.Length == 1) 
		{
			canOperate = true;
			Touch theTouch = Input.GetTouch (0);
			Vector2 touchControlVector = new Vector2 (theTouch.deltaPosition.x , theTouch.deltaPosition.y);
			MoveWithVector (touchControlVector);
		} 
		else if (Input.touches.Length == 2 ) 
		{
			Touch theTouch;
			if (SystemValues.IsOperatingUI () && !SystemValues.IsOperatingUI (Input.GetTouch (1).fingerId)) 
			{
				canOperate = true;
				theTouch = Input.GetTouch (1);
				Vector2 touchControlVector = new Vector2 (theTouch.deltaPosition.x, theTouch.deltaPosition.y);
				MoveWithVector (touchControlVector);
			}
			if (!SystemValues.IsOperatingUI () && SystemValues.IsOperatingUI (Input.GetTouch (1).fingerId)) 
			{
				canOperate = true;
				theTouch = Input.GetTouch (0);
				Vector2 touchControlVector = new Vector2 (theTouch.deltaPosition.x, theTouch.deltaPosition.y);
				MoveWithVector (touchControlVector);
			}
		} 
		else
		{
			canOperate = false;
		}
	}

	/// <summary>
	/// 摄像机的鼠标操作
	/// </summary>
	private void CameraOperate()
	{
		if (!theTarget) 
		{
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
				Vector2 mouseControl = new Vector2 (xMove , yMove);
				MoveWithVector (mouseControl );
				mousePosition = Input.mousePosition;
			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			canOperate = false;
		}
	}
//----------------------------------------------------操作方法封装，获得输入------------------------------------------------------------------------------//

	/// <summary>
	/// 战斗/移动模式之下的摄像机操作
	/// </summary>
	private void MoveCamera()
	{
		if (SystemValues.theCameraState == CameraState.rotateCamera || canOperate) 
		{
			//Vector3 aimPosition = theTarget.transform.position + extraDistance;//这一步的计算和赋值其实没有什么用，暂时先不做
			Vector3 aimPosition = new Vector3 (this.transform.position.x, theTarget.transform.position.y + height, this.transform.position.z);
			extraDistance = (aimPosition - theTarget.transform.position).normalized * distance;

			//先移动摄像机再看
			FixPosition ();
			this.transform.LookAt (theTarget.transform.position + new Vector3 (0f, 1f, 0f));
		} 
		else if (SystemValues.theCameraState == CameraState.fixedCamera) 
		{
			FixPosition ();
		}
	}


	/// <summary>
	/// 最终移动摄像机的方法
	/// </summary>
	private void FixPosition()
	{
		//不能用插值，会有动荡
		if(!canOperate)
			this.transform.position = theTarget.transform.position + extraDistance;
		else
			this.transform.position = Vector3.Lerp(this.transform.position ,  theTarget.transform.position + extraDistance , 0.5f);
	}


	/// <summary>
	/// 封装的touch移动方法
	/// 传入的参数就是Touch
	/// 以操纵的上下输入作为参数的综合方法
	/// </summary>
	/// <param name="theTouch">The touch.</param>
	private void MoveWithVector(Vector2 theControlVector)
	{
		float xMove = theControlVector.x; 
		float yMove = theControlVector.y;
		//0.008f等等数值用于调整操作顺滑度
		xMove = Mathf.Abs (xMove) > 3f ? xMove * 0.008f : 0f;
		yMove = Mathf.Abs (yMove) > 3f ? yMove * 0.006f : 0f;
		//取反是可以改变操作的感觉
		xMove = -xMove;
		yMove = -yMove;

		if (ChangeFov (theControlVector)) 
		{
			height += yMove;
			distance += yMove;
			height = Mathf.Clamp (height, minHeight, maxHeight);
			distance = Mathf.Clamp (distance, minDistance, maxDistance);
		}

		this.transform.Translate (new Vector3 (xMove, 0f, 0f));
	}

	/// <summary>
	///附加，修改摄像机的视野范围的方法
	/// 只有在摄像机在最下面的时候才可以触发
	/// 在调整视野范围的时候是不可以进行高度移动的
	/// </summary>
	/// <param name="theControlVector">The control vector.</param>
	private bool ChangeFov(Vector2 theControlVector)
	{
		if (height > minHeight)
			return true;
		
		theControlVector = -theControlVector;
		//上下移动操作的条件
		if (theControlVector.y > 0 && Mathf.Abs (theCamera.fieldOfView - 60f) < 0.35f) 
			 return true;

		theCamera.fieldOfView += theControlVector.y * 0.1f;
		theCamera.fieldOfView = Mathf.Clamp (theCamera.fieldOfView, minFov, maxFov);
		return false;
	}

	//-------------------------------------------------------剧情摄像机----------------------------------------------------------//
	/// <summary>
	/// 剧情模式之下的摄像机操作
	/// </summary>
	private float cameraFovSave;
	private void PlotCamera()
	{
		if (!theTarget)
			return;

		cameraFovSave = theCamera.fieldOfView;
		theCamera.fieldOfView = maxFov;

		if(Random.value < 0.5f)
			this.transform.position = theTarget.transform.position + theTarget.transform.rotation * new Vector3 (2f,2f,2f) ;
		else
			this.transform.position = theTarget.transform.position + theTarget.transform.rotation * new Vector3 (-2f,2f,2f) ;
		
		this.transform.LookAt (theTarget.transform.position  + theTarget.transform.rotation *  new Vector3(0f,1.25f,1f) );
	}

	/// <summary>
	/// 在进入剧情的时候可以发生的事情
	/// </summary>
	public void OnIntoPlot()
	{
		isPloting = true;
		PlotCamera ();
	}


	/// <summary>
	/// 离开剧情模式下的额外操作
	/// </summary>
	public void OnOutPlot()
	{
		isPloting = false;
		this.transform.position = theTarget.transform.position + extraDistance;
		this.transform.LookAt (theTarget.transform.position + new Vector3 (0f, 1f, 0f));
		theCamera.fieldOfView = cameraFovSave;

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
	}
		
}
	
