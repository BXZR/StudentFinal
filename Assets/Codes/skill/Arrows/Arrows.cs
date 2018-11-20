using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour {

	public float arrowSpeed = 25f;//弹矢速度
	TrailRenderer theRender;
	public Player thePlayer;

	void Start()
	{
		theRender = this.GetComponent<TrailRenderer> ();
	}

	void Update () 
	{
		this.transform.Translate (new Vector3 (0,0,1) * arrowSpeed  *Time.deltaTime);
	}


	void OnTriggerEnter(Collider collisioner)
	{
		print ("触发攻击");
		Player playeraim = collisioner.GetComponent<Player> ();
		if (playeraim  && this.thePlayer && playeraim != this.thePlayer) 
		{

			this.thePlayer.OnAttack (playeraim);
		}
	}



	void OnEnable()
	{
		if (theRender)
		{
			theRender.enabled = true;
		}
	}

	void OnDisable()
	{
		if (theRender)
		{
			//theRender.Clear();
			theRender.enabled = false;
		}
	}

}
