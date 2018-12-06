using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterWall : MonoBehaviour {

	//这个墙是用来触发怪物计算的
	//主人公撞到这个墙就会激活所有里面的怪

	void OnTriggerEnter(Collider collisioner)
	{
		if (collisioner.tag == "Player") 
		{
			FSMStage[] FS = this.GetComponentsInChildren<FSMStage> ();
			for (int i = 0; i < FS.Length; i++)
				FS [i].enabled = true;
			Destroy (this);
		}
	}

}
