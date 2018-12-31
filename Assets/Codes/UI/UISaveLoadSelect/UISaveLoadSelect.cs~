using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class UISaveLoadSelect : UIBasic {

	//存档按钮截图
	public Image  [] saveButtonImages;
	public Text[] saveButtonTexts;
	public Text operateTypeText;
	public Camera theCamera;

	//存档图像参数
	private  int width = 180;
	private  int height = 150;

	//当前的模式：存档还是读档
	private int modeNow = 999;//0 存档 1 读档

	public override void OnShow (string value = "")
	{
		theCamera.transform.position = Camera.main.transform.position;
		theCamera.transform.rotation = Camera.main.transform.rotation;
		if (value.Equals ("Save"))
			modeNow = 0;
		else if (value.Equals ("Load"))
			modeNow = 1;

		operateTypeText.text = modeNow == 0 ? "存档" : "读档";

		Time.timeScale = 1f;
		Invoke ("loadPictureOnStart" , 0.01f);
	}
	public override void OnEndShow ()
	{
		Time.timeScale = 0f;
	}



	/// <summary>
	/// 外部按钮方法
	/// </summary>
	public void ButtonOperate(int ID )
	{
		if (modeNow == 0)
			MakeSave (ID);
		else if (modeNow == 1)
			MakeLoad (ID);
		else
			UIController.GetInstance ().ShowUI<messageBox> ("无效操作");
	}

    /// <summary>
    /// 按钮方法：进行存档
    /// </summary>
	private void MakeSave(int ID)
	{
		bool saveOp = SystemValues.SaveInformation (ID.ToString());
		StartCoroutine (OnScreenCapture2 (ID));
		string show = saveOp ? "存档成功" : "存档失败";
		UIController.GetInstance ().ShowUI<messageBox> (show);
		Invoke ("loadPictureOnStart" , 0.01f);

	}

	/// <summary>
	///按钮方法，进行读档
	/// </summary>
	private  void MakeLoad(int ID)
	{
		bool loadOp = SystemValues.LoadInformation (ID.ToString());
		string show = loadOp ? "读取成功" : "读取失败";
		UIController.GetInstance ().ShowUI<messageBox> (show);
	}



	private void loadPictureOnStart()
	{
		StartCoroutine (loadPicture());
	}

	//获取存档截图
	IEnumerator loadPicture()
	{
		//yield return  new WaitForSeconds (0.01f);
		//一共就只有三个存档
		for (int i = 0; i < 3; i++) 
		{
			string path = @"file:///" + Application.persistentDataPath + "/save" + i + ".jpg";
			string checkPath = Application.persistentDataPath + "/save" + i +  ".jpg";
			//print (path);
			if (File.Exists (checkPath))
			{
				WWW theWWW = new WWW (path);
				yield return theWWW;
				//加载图片
				Texture2D theTextureIn = theWWW.texture;

				if (theTextureIn != null && saveButtonImages[i] != null) 
					saveButtonImages[i].sprite = Sprite.Create (theTextureIn, new Rect (0, 0, theTextureIn.width, theTextureIn.height), new Vector2 (0, 0));
				else 
					print ("load fail!");
				
				//加载修改时间
				FileInfo fi = new FileInfo (checkPath);
				if (saveButtonTexts[i])
					saveButtonTexts[i].text = fi.LastWriteTime.ToString ();
			}
		}
	}


	//缩放的过程
	private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

		float incX = (1.0f / (float)targetWidth);
		float incY = (1.0f / (float)targetHeight);

		for (int i = 0; i < result.height; ++i)
		{
			for (int j = 0; j < result.width; ++j)
			{
				UnityEngine.Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
				result.SetPixel(j, i, newColor);
			}
		}
		result.Apply();
		return result;
	}

	//目前真正使用的方法
	public  IEnumerator OnScreenCapture2 (int index )
	{
		string path  = Application .persistentDataPath+"/save"  + index + ".jpg";
		//短暂的等待
		yield return new WaitForEndOfFrame();
		try
		{
			int widthUse = Screen.width;
			int heightUse = Screen.height;

			RenderTexture rt = new RenderTexture( widthUse, heightUse, 0);

			theCamera.targetTexture = rt;
			theCamera.Render();
			RenderTexture.active = rt;

			Texture2D tex =  new Texture2D(widthUse,heightUse, TextureFormat.RGB24, false);//新建一张图
			tex.ReadPixels (new Rect (0, 0, widthUse, heightUse), 0, 0, true);//从屏幕开始读点

			tex = ScaleTexture(tex , width ,  height);//缩放的过程

			byte[] imagebytes = tex.EncodeToJPG ();//用的是JPG(这种比较小)
			//使用它压缩实时产生的纹理，压缩过的纹理使用更少的显存并可以更快的被渲染
			//通过true为highQuality参数将抖动压缩期间源纹理，这有助于减少压缩伪像
			//因为压缩后的图像不作为纹理使用，只是一张用于展示的图
			//但稍微慢一些这个小功能暂时貌似还用不到
			tex.Compress (false);
			tex.Apply();
			Texture2D mScreenShotImgae = tex;

			try
			{
				File.WriteAllBytes (path, imagebytes);
			}
			catch
			{
				print("保存存档截图失败");
			}
			mScreenShotImgae = tex;
		}
		catch (System.Exception e)
		{
			print ("截图失败");
		}
	}

	void Update()
	{
		print ("dfdffgrth");
	}
}
