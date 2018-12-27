using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTalkCanvas : UIBasic {

	//游戏感言界面
	//没办法，我就是喜欢说感言
	//有太多的东西想要倾诉
	//有太多的东西想要表达

	public Transform theShower;
	public float moveSpeed = 2f;
	public float textMoveDistance = 100f;
	private Vector3 textStartPosition;
	public  Vector3 textEndPosition;
	private bool isStarted = false;

	void Start () 
	{
		textStartPosition = theShower.transform.position;
		textEndPosition = textStartPosition + new Vector3 ( textMoveDistance  , 0f , 0f);
		theShower.transform.position = textStartPosition;
		isStarted = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distanceNow = Vector3.Distance (theShower.transform.position, textEndPosition);
		print ("distance = "+distanceNow);
		if( Mathf.Abs(distanceNow) > 5)
			theShower.transform.Translate (Vector3.right* moveSpeed *Time.deltaTime);
	}

	void OnEnable()
	{
		if(isStarted)
			theShower.transform.position = textStartPosition;
	}
}
