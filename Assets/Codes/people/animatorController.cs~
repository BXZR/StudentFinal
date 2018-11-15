using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerAction {idle, move, attack ,jump}

public class animatorController : MonoBehaviour {

	//这个类用于控制动画的释放
	private Animator theAnimator;

	void Start ()
	{
		theAnimator = this.GetComponent <Animator> ();	
	}


    //其实很烂的动画转折方法
	public void PlayAnimation(playerAction theAction)
	{
		switch (theAction)
		{
		case playerAction.attack:
			{
				theAnimator.Play ("attack");
				break;
			}
		case playerAction.move:
			{
				theAnimator.Play ("move");
				break;
			}
		case playerAction.idle:
			{
				theAnimator.Play ("idle");
				break;
			}
		case playerAction.jump:
			{
				theAnimator.Play ("jump");
				break;
			}
		}
	}


	public void setValue(float x , float y)
	{
		theAnimator.SetFloat ("forward", y);//播放动画,具体内容需要看controller 
		theAnimator.SetFloat ("right", x);//播放动画,具体内容需要看controller 
	}

	public void setValue(Vector3 move)
	{
		theAnimator.SetFloat ("forward", move.z );//播放动画,具体内容需要看controller 
		theAnimator.SetFloat ("right", move.x);//播放动画,具体内容需要看controller 
	}


}
