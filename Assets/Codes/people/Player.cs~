using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public SkillBasic theSkillNow = null;

	public  void SkillEffect (float extradamage)
	{
		if (theSkillNow != null)
			theSkillNow.SkillEffect (extradamage);
		theSkillNow = null;
	}

	void Start () {
		SystemValues.thePlayer = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
