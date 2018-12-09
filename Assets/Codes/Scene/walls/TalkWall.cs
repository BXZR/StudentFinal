using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkWall : MonoBehaviour {

	//触发剧情对话的墙壁
	public int PlotType;
	public int PlotID;
	public bool isAutoDestroy = true;
	public bool isExitMode = false;//在一个范围内走出去的时候触发

	void OnTriggerEnter(Collider collisioner)
	{
		if (isExitMode)
			return;
		
		if (collisioner.tag == "Player") 
			ShowPlot ();
	}

	void OnTriggerExit(Collider collisioner)
	{
		if (!isExitMode)
			return;

		if (collisioner.tag == "Player") 
			ShowPlot ();
	}


	private void ShowPlot()
	{
		string plotName = SystemValues.getPlotName (PlotType , PlotID);
		//print (plotName  +"---");
		if (string.IsNullOrEmpty (plotName))
			return;
		
		UIController.GetInstance ().ShowUI<TalkCanvas> (plotName);
		if(isAutoDestroy)
			Destroy (this.gameObject);
	}
}
