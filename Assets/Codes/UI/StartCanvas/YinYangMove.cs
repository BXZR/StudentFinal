using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YinYangMove : MonoBehaviour {

	//阴阳图的移动
	public float moveDistance = 2f;

	//两个坐标，分别是起点和终点
	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 aimNow;//当前移动目标
	private bool isMoving = false;


	void Start () 
	{
		startPosition = this.transform.position;
		endPosition = this.transform.position + new Vector3 (moveDistance , 0f , 0f);
		aimNow = startPosition;

		StartMove ();
	}


	public void StartMove()
	{
		isMoving = true;
		aimNow = aimNow == startPosition ? endPosition : startPosition;
		CancelInvoke ();
		Invoke ("endMove" , 5f);
	}

	private void  endMove()
	{
		isMoving = false;
	}

	void Update()
	{ 
		if(isMoving)
		this.transform.position = Vector3.Lerp (this.transform.position ,  aimNow , 0.2f);
	}

}
