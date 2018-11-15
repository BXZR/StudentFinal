using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUI : MonoBehaviour {

	public Sprite RotateCameraPicture;
	public Sprite FixedCameraPicture;
	private Image theButtonImage;


	void Start()
	{
		theButtonImage = this.GetComponent<Image> ();
		ShowUI ();
	}

	//切换摄像机模式的UIButton
	public void ChangeCameraMode()
	{
		//切换模式
		SystemValues.ChangeCameraMode();
		//更换图片
		ShowUI();
	}

	/// <summary>
	/// 根据摄像机状态切换图片.
	/// </summary>
	private void ShowUI()
	{
		if (SystemValues.theCameraState == CameraState.fixedCamera)
			theButtonImage.sprite = FixedCameraPicture;
		else
			theButtonImage.sprite = RotateCameraPicture;
	}
}
