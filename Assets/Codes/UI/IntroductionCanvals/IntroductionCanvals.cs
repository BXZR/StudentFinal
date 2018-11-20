using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionCanvals : UIBasic {

	//信息介绍面
	public Text theTitleText;
	public Text theInformationText;

	public override void OnShow (string value = "")
	{
		string[] texts = value.Split (',');
		if (texts.Length < 2)
			this.gameObject.SetActive (false);
		
		theTitleText.text = texts [0];
		theInformationText.text = texts[1];
	} 

}
