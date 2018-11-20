using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTalkCanvas : UIBasic {

	//游戏感言界面
	//没办法，我就是喜欢说感言
	//有太多的东西想要倾诉
	//有太多的东西想要表达

	public Transform theText;
	public float moveSpeed = 2f;
	public float textMoveDistance = 100f;
	private Vector3 textStartPosition;
	public  Vector3 textEndPosition;
	private bool isStarted = false;

	void Start () 
	{
		textStartPosition = theText.transform.position;
		textEndPosition = textStartPosition + new Vector3 (0f , textMoveDistance , 0f);
		theText.transform.position = textStartPosition;
		isStarted = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(theText.transform.position , textEndPosition) > 0.5f)
			theText.transform.Translate (Vector3.up * moveSpeed *Time.deltaTime);
	}

	void OnEnable()
	{
		if(isStarted)
		 theText.transform.position = textStartPosition;
	}
}
