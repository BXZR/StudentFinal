using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIBasic {

	private Slider theSlider;
	float valueShow = 0f;
	private string aimScene = ""; 
	public Image backImage;
	Sprite  []pics = null;

	private void LoadPictures()
	{
		pics =  Resources.LoadAll<Sprite> ("LoadingPictures");
	}

	public override void OnShow (string value = "")
	{
		if (pics == null)
			LoadPictures ();

		backImage.sprite = pics [Random.Range (0, pics.Length)];

		Time.timeScale = 1f;
		if (!theSlider)
			theSlider = this.transform.GetComponentInChildren<Slider> ();
		theSlider.value = 0f;
		aimScene = value;
		StartCoroutine(startLoading());
	}


	IEnumerator startLoading()//异步加载新的场景
	{
		//异步加载场景
		AsyncOperation acop = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (aimScene);
		//因为太快了所以需要加一点缓冲
		acop.allowSceneActivation = false;

		for (int i = 0; i < 20; i++) {
			valueShow += 0.05f;
			theSlider.value = valueShow;
			yield return new WaitForSeconds (0.05f);
		}
		acop.allowSceneActivation = true;

		yield return acop;
	}

}
