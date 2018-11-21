using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkWall : MonoBehaviour {

	//触发剧情对话的墙壁
	public string talkName;
	public bool isAutoDestroy = true;
	public bool isExitMode = false;//在一个范围内走出去的时候触发

	void OnTriggerEnter(Collider collisioner)
	{
		if (isExitMode)
			return;
		
		if (collisioner.tag == "Player") 
		{
			UIController.GetInstance ().ShowUI<TalkCanvas> (talkName);
			if(isAutoDestroy)
			    Destroy (this.gameObject);
		}
	}

	void OnTriggerExit(Collider collisioner)
	{
		if (!isExitMode)
			return;

		if (collisioner.tag == "Player") 
		{
			UIController.GetInstance ().ShowUI<TalkCanvas> (talkName);
			if(isAutoDestroy)
				Destroy (this.gameObject);
		}
	}
}
