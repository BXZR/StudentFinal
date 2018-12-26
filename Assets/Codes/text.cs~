using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Z ))
			SystemValues.SaveInformation ();

		if (Input.GetKeyDown (KeyCode.X ))
			SystemValues.LoadInformation ("0");
		if (Input.GetKeyDown (KeyCode.V))
		{
			Mission_KillMonster1 A = new Mission_KillMonster1 ();
			A.MakeStart ();
			SystemValues.thePlayer.GetComponent<Player> ().theMissionPackage.AddNewMission (A);
		}

		if (Input.GetKeyDown (KeyCode.B))
			UIController.GetInstance ().ShowUI<UIMissionCanvas> ();
	}
}
