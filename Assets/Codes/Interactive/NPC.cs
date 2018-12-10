using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractiveBasic{

	//NPC需要进入对话
    
	public string NPCPlotName = "";//剧本名
	public bool isAutoDestroy = true;//可以重复对话

	void Start()
	{
		SetTag ();
	}

	//进行交互
	public override void MakeInteractive ()
	{
		if (!string.IsNullOrEmpty (NPCPlotName)) 
		{
			UIController.GetInstance ().ShowUI<TalkCanvas> (NPCPlotName);
			Vector3 point = SystemValues.thePlayer.transform.position;
			Vector3 pointNPC = this.transform.position;
			this.transform.LookAt (new Vector3( point.x , pointNPC.y , point.z));
			SystemValues.thePlayer.transform.LookAt (new Vector3(pointNPC.x , point.y , pointNPC.z));
			if(isAutoDestroy)
			    Destroy (this);
		}
	}
}
