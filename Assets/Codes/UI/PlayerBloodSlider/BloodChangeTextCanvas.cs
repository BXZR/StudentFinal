using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodChangeTextCanvas : MonoBehaviour {

	private static List<BloodChangeTextCanvas> bloodtexts = new List<BloodChangeTextCanvas> ();
	public static BloodChangeTextCanvas GetText()
	{
		bloodtexts.RemoveAll (X => X== null);
		if (bloodtexts.Count > 0) 
		{
			BloodChangeTextCanvas aTextCanvas = bloodtexts [0];
			bloodtexts.Remove (aTextCanvas);
			return aTextCanvas;
		}

		BloodChangeTextCanvas newOne = Instantiate( SystemValues.LoadResources<GameObject>("UI/BloodChangeTextCanvas")).GetComponent<BloodChangeTextCanvas>();
		return newOne;
	}


	private Color playerUpColor = Color.green;
	private Color monsterUpColor = Color.cyan;
	private Color playerDownColor = Color.red;
	private Color monsterDownColor = Color.yellow;
	private Text  theText;

	public void MakeShow(float value)
	{
		CancelInvoke ();

		if (!theText)
			theText = this.GetComponentInChildren<Text> ();

		if (value == 0)
		{
			MakeEnd ();
			return;
		}


		theText.text = value.ToString ("f0");
		if (value > 0) 
		{
			if (this.transform.root.tag.Equals ("Player"))
				theText.color = playerUpColor;
			else
				theText.color = monsterUpColor;
		} 
		else if (value < 0) 
		{
			if (this.transform.root.tag.Equals ("Player"))
				theText.color = playerDownColor;
			else
				theText.color = monsterDownColor;
		}

		this.transform.localPosition = Vector3.one * -0.2f ;
		this.transform.localRotation = Quaternion.identity;
		this.transform.localScale = Vector3.one;
		this.gameObject.SetActive (true);

		Invoke ("MakeEnd" , 0.5f);
	}

	private void MakeEnd()
	{
		this.gameObject.SetActive (false);
		bloodtexts.Add (this);
	}

	void Update()
	{
		this.transform.Translate (Vector3.up * 2f * Time.deltaTime);
	}
}
