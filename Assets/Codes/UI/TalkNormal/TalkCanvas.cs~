using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class TalkCanvas : UIBasic {

	//显示对话
	private XmlDocument xml = new XmlDocument();
	private XmlNodeList theXmlList;
	private Queue<DialogFrame>theFrames = new Queue<DialogFrame> ();

	public Text theNameText;//名字
	public Text theInformationText;//对话
	public Image theTalkHeadPicture;//头像


	public override void OnShow (string value = "")
	{
		UIController.GetInstance().CloseUI<PlayerActCanvas>();//对话的时候不可以操作
		LoadTexts(value);//加载对话内容
		ShowText();//加载第一句话
	} 


		

	private void LoadTexts(string value)
	{
		theFrames = new Queue<DialogFrame> ();
		TextAsset textAsset = (TextAsset)Resources.Load ("XML/" + value);
		xml.LoadXml (textAsset.text);
		theXmlList = xml.SelectNodes ("Root/Dialog");
		foreach (XmlNode node in theXmlList) 
		{
			DialogFrame aFrame = new DialogFrame();
			//print (node.SelectSingleNode("Name").InnerText);
			//print (node.SelectSingleNode("Picture").InnerText);
			//print (node.SelectSingleNode("Information").InnerText);
			aFrame.name = node.SelectSingleNode("Name").InnerText;
			aFrame.picture = node.SelectSingleNode ("Picture").InnerText;
			aFrame.information = node.SelectSingleNode ("Information").InnerText;
			theFrames.Enqueue (aFrame);
		}
	}


	//点击生效
	public void ShowText()
	{
		if (theFrames.Count == 0) 
		{
			UIController.GetInstance ().ShowUI<PlayerActCanvas> ();
			UIController.GetInstance ().CloseUI<TalkCanvas> ();
		} 
		else 
		{
			DialogFrame use = theFrames.Dequeue ();
			theNameText.text = use.name;
			theInformationText.text = use.information;

			use.picture =  ! string.IsNullOrEmpty (use.picture) ? use.picture : "noOne";
			Texture2D theTextureIn = Resources.Load <Texture2D> ("TalkPicture/" + use.picture);
			Sprite theSprite = Sprite.Create (theTextureIn, new Rect (0, 0, theTextureIn.width, theTextureIn.height), new Vector2 (0, 0));
			theTalkHeadPicture.sprite = theSprite;

		}

	}




}


class DialogFrame
{
	public string name;
	public string picture;
	public string information;
}