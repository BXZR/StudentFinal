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

	void Start ()
	{
		canOperate = true;
		FixedPostion ();
		canOperate = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CameraOperate ();
	}

	void LateUpdate()
	{
		FixedPostion ();
	}

	//摄像机的鼠标操作
	private void CameraOperate()
	{
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

	//摄像机的青位置修订操作
	private void FixedPostion()
	{
		if (SystemValues.theCameraState == CameraState.rotateCamera || canOperate) 
		{
			Vector3 aimPosition = theTarget.transform.position + extraDistance ;
			aimPosition =  new Vector3 (this.transform.position.x , theTarget.transform.position.y + height, this.transform.position.z);
			extraDistance =  (aimPosition - theTarget.transform.position).normalized * distance;
			this.transform.LookAt (theTarget.transform.position + new Vector3 (0f, 1f, 0f));
			this.transform.position = Vector3.Lerp(this.transform.position ,  theTarget.transform.position + extraDistance , 0.8f);
		}
		else if (SystemValues.theCameraState == CameraState.fixedCamera) 
		{
			this.transform.position = theTarget.transform.position + extraDistance;
			//Vector3.Lerp(this.transform.position ,  theTarget.transform.position + extraDistance , 0.8f);
		}

	}
		
}
