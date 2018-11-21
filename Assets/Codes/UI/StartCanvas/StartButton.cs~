using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	//开始界面的按钮事件方法都在这里了

	void Start () {
		this.GetComponent<Image> ().alphaHitTestMinimumThreshold = 0.1f;
	}

	//游戏感言
	public void ShowGameTalk()
	{
		UIController.GetInstance ().ChangeUIState<GameTalkCanvas> ();
	}

	//跳转场景
	public void SceneSkip(string aimScene)
	{
		try{UIController.GetInstance().ShowUI<UILoading>(aimScene);}
		catch{UIController.GetInstance ().ShowUI<messageBox> ("找不到指定场景");}
	}
}
