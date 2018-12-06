using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForwardBack : MonoBehaviour {

	//适用于自行移动的场景内容
	//来回移动

	private Vector3 aimForward;
	private Vector3 aimStart;
	private Vector3 aimNow;
	public float distance = 0.5f;
	void Start () 
	{
		aimStart = this.transform.position;
		aimForward = this.transform.position + new Vector3 (0f, 0f, distance);
		aimNow = aimForward;
		InvokeRepeating ("MakeMove" , 0f , 0.5f);
	}


	private void  MakeMove()
	{
		this.transform.position = Vector3.Lerp (this.transform.position , aimNow , 1f);
		if (Vector3.Distance (aimNow, this.transform.position) < 0.05f)
		{
			print (aimNow + "--");
			aimNow = aimNow == aimForward ? aimStart : aimForward;
			print (aimNow);
		}
	}

}
