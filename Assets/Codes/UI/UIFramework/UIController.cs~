using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	private static UIController theUIcontroller;

	public static UIController GetInstance()
	{
		return theUIcontroller;
	}

	private Dictionary <string , GameObject> UIBook = new Dictionary<string, GameObject>();

	public  void ShowUI <T> (string value = "") where T : UIBasic
	{
		string UIName = typeof(T).ToString ();
		GameObject theUI;
		if (!UIBook.TryGetValue (UIName, out theUI))
		{
			theUI = SystemValues.LoadResources<GameObject>("UI/" + UIName);
			theUI = Instantiate (theUI);
			theUI.name = UIName;
			UIBook.Add (UIName, theUI);
		}
		if(theUI)
		{
			theUI.GetComponent <T> ().OnShow (value);
			theUI.SetActive (true);
		}
	}

	public  void CloseUI<T> () where T: UIBasic
	{
		
		string UIName = typeof(T).ToString ();
		GameObject theUI;
		if (!UIBook.TryGetValue (UIName, out theUI))
		{
			theUI = SystemValues.LoadResources<GameObject>("UI/" + UIName);
			theUI = Instantiate (theUI);
			theUI.name = UIName;
			UIBook.Add (UIName, theUI);
		}
		if(theUI)
		{
			theUI.GetComponent <T> ().OnEndShow ();
			theUI.SetActive (false);
		}
	}

	/// <summary>
	/// 第一次生成UI，之后开/关UI
	/// </summary>
	public void ChangeUIState<T> ( ) where T : UIBasic
	{
		string UIName = typeof(T).ToString ();
		GameObject theUI;
		if (!UIBook.TryGetValue (UIName, out theUI))
		{
			theUI = SystemValues.LoadResources<GameObject>("UI/" + UIName);
			theUI = Instantiate (theUI);
			theUI.name = UIName;
			UIBook.Add (UIName, theUI);
			if(theUI)
				theUI.SetActive (false);
		}
		if(theUI)
		{
			theUI.SetActive (!theUI.gameObject.activeInHierarchy);
		}
	}


	void Start()
	{
		Screen.SetResolution (1280 , 800, true, 60);
		theUIcontroller = this;
	}
//	void Update()
//	{
//		if (Input.GetKeyDown (KeyCode.A))
//			ShowUI<messageBox> ("这个世界是有真理的");
//		if (Input.GetKeyDown (KeyCode.S))
//			ShowUI<messageBox> ("这个真理毫无疑问就是吸");
//	}
}
