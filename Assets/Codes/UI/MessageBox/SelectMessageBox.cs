using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void MesageOperate();
public class SelectMessageBox : UIBasic {

	//额外的操作
	public MesageOperate theOperate;

	public Text theText;

	public override void OnShow (string value = "")
	{
		theText.text = value;
	} 


	public void Yes()
	{
		theOperate ();
	}

	public void No()
	{
		this.gameObject.SetActive (false);
	}

}
