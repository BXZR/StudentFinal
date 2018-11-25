using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractiveBasic{

	//NPC需要进入对话
    
	public string NPCPlotName = "";//剧本名

	//进行交互
	public override void MakeInteractive ()
	{
		if (!string.IsNullOrEmpty (NPCPlotName)) 
		{
			UIController.GetInstance ().ShowUI<TalkCanvas> (NPCPlotName);
			Destroy (this);
		}
	}
}
