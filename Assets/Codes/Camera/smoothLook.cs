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
	public Vector3 extraDistance = new Vector3 (0,6,-5);
	//当前是否是剧情摄像机
	//进入剧情和离开剧情的时候需要修改这个标记
	public bool isPloting = false;

	void Start ()
	{
		SystemValues.theCamera = this;
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


	/// <summary>
	/// 摄像机的触摸操作
	/// 进化版，可以检测多个手指的移动
	/// </summary>
	private void CameraOperateTouch()
	{
		if (!theTarget)
			return;

		if (SystemValues.IsOperatingUI () && Input.touches.Length >= 2 && !SystemValues.IsOperatingUI(Input.GetTouch (1).fingerId)) 
		{
			canOperate = true;
			Touch theTouch = Input.GetTouch (1);
			Vector2 touchControlVector = new Vector2 (theTouch.deltaPosition.x , theTouch.deltaPosition.y);
			MoveWithVector (touchControlVector);
		} 
		else if (!SystemValues.IsOperatingUI () && Input.touches.Length >= 1) 
		{
			canOperate = true;
			Touch theTouch = Input.GetTouch (0);
			Vector2 touchControlVector = new Vector2 (theTouch.deltaPosition.x , theTouch.deltaPosition.y);
			MoveWithVector (touchControlVector);
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
	/// </summary>
	/// <param name="theTouch">The touch.</param>
	private void MoveWithVector(Vector2 theControlVector)
	{

		float xMove = theControlVector.x; 
		float yMove = theControlVector.y;
		xMove = Mathf.Abs (xMove) > 3f ? xMove * 0.008f : 0f;
		yMove = Mathf.Abs (yMove) > 3f ? yMove * 0.006f : 0f;
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
	/// 剧情模式之下的摄像机操作
	/// </summary>
	private void PlotCamera()
	{
		if (!theTarget)
			return;

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
	
