using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveJoyStick : MonoBehaviour {

	//这个类是摇杆类
	//摇杆应该是自治的，每一次传入一个Player就可以操纵了
	private move theMovePlayer = null;//受到操控的Player，这个可以想个办法自己获取到


	//初始化
	private void MakeStart()
	{
		if (theMovePlayer != null)
			return;
		
		theMovePlayer = SystemValues.thePlayer.GetComponent<move> ();
	}

	//包装好的移动操纵方法
	public void OnMoveStart()
	{
		MakeStart ();
		if(theMovePlayer)
		    theMovePlayer.startMoving ();
	}
	public void OnMove(Vector2 theAxis)
	{
		if(theMovePlayer)
		     theMovePlayer.InputOperateWithAxis (theAxis);
	}
	public void OnEndMoving()
	{
		if(theMovePlayer)
		    theMovePlayer.stopMoving ();
	}


}
