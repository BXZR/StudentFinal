using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITalkSelect : UIBasic {

	//有的时候对话是需要选择的
	//暂定一共只有两个选择

	public Text theText1;//选择的文字1
	public Text theText2;//选择的文字2

	private string aimPlot1 = "";
	private string aimPlot2 = "";

	public override void OnShow (string value = "")
	{
		string[] values = value.Split (',');
		if (values.Length < 4)
		{
			UIController.GetInstance ().CloseUI<UITalkSelect> ();
		}
		else 
		{
			theText1.text = values [0];
			theText2.text = values [1];
			aimPlot1 = values [2];
			aimPlot2 = values [3];
		}


	}


	//如果选择了路线1，发生的事情
	public void SelectAim1()
	{
		UIController.GetInstance ().CloseUI<UITalkSelect> ();
		UIController.GetInstance ().ShowUI<TalkCanvas> (aimPlot1);
	}

	//如果选择了路线1，发生的事情
	public void SelectAim2()
	{
		UIController.GetInstance ().CloseUI<UITalkSelect> ();
		UIController.GetInstance ().ShowUI<TalkCanvas> (aimPlot2);
	}
}
