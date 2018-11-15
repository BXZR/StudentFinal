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
	private float ySave = 0;//储存的Y坐标，只有超过一定阈值长期可以更新

	//完全固定视角摄像机
	public Vector3 extraDistance = new Vector3 (0,2,-2);

	void Start ()
	{
		ySave = theTarget.transform.position.y + height;
		canOperate = true;
		FixedPostion ();
		canOperate = false;
		FixedPostion ();
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
				xMove = Mathf.Abs (xMove) > 8f ? xMove * 0.02f : 0f;
				yMove = Mathf.Abs (yMove) > 8f ? yMove * 0.01f : 0f;

				mousePosition = Input.mousePosition;
				this.transform.Translate (new Vector3 (xMove, 0f, 0f));
				height += yMove;
				distance += yMove;
				height = Mathf.Clamp (height , 2f, 6f);
				distance = Mathf.Clamp (distance  , 4f , 8f);
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
			this.transform.LookAt (theTarget.transform.position + new Vector3 (0f, 1f, 0f));
			Vector3 aimPosition = theTarget.transform.position + (this.transform.position - theTarget.transform.position).normalized * distance;
			this.transform.position =Vector3.Lerp (this.transform.position, aimPosition  , 0.6f);
			ySave = Mathf.Lerp (ySave, theTarget.transform.position.y + height, 0.1f);
			this.transform.position = new Vector3 (this.transform.position.x, ySave, this.transform.position.z);

			extraDistance =  (this.transform.position - theTarget.transform.position).normalized * distance;
		}
		if (SystemValues.theCameraState == CameraState.fixedCamera) 
		{
			this.transform.position = Vector3.Lerp (this.transform.position, theTarget.transform.position + extraDistance, 0.6f);
		}
	}
		
}
