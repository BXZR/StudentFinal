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
		UIController.GetInstance().CloseUI<HpBasicPanel>();//基本生命值界面也关闭
		LoadTexts(value);//加载对话内容
		ShowText();//加载第一句话
		SystemValues.theCamera.OnIntoPlot();
		SystemValues.CloseBloodCanvas ();
	} 

	public override void OnEndShow ()
	{
		SystemValues.theCamera.OnOutPlot ();
	}

	/// <summary>
	/// 可以外部控制的直接关闭
	/// </summary>
	public void MakeJustClose()
	{
		if (theFrames.Count >= 0) 
		{
			DialogFrame use = theFrames.Dequeue ();
			while (theFrames.Count > 0)
				use = theFrames.Dequeue ();
			OperateFrame (use);
		} 
		UIController.GetInstance ().ShowUI<PlayerActCanvas> ();
		UIController.GetInstance ().ShowUI<HpBasicPanel> ();
		UIController.GetInstance ().CloseUI<TalkCanvas> ();
	}

	private void LoadTexts(string value)
	{
		//print ("value =" +value);
		theFrames = new Queue<DialogFrame> ();
		TextAsset textAsset = SystemValues.LoadResources<TextAsset>("XML/" + value);
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
			UIController.GetInstance ().ShowUI<HpBasicPanel> ();
			UIController.GetInstance ().CloseUI<TalkCanvas> ();
		}
		else 
		{
			DialogFrame use = theFrames.Dequeue ();
			OperateFrame (use);
		}

	}

	/// <summary>
	/// 对每一个剧本帧的操作
	/// </summary>
	private void OperateFrame(DialogFrame use)
	{
		switch (use.name)
		{
		case "SELECT":
			makeSelect (use);
			break;
		case "GOLD":
			makeOver (use);
			break;
		case "MISSION":
			makeMission (use);
			break;
		default:
			makeTalk (use);
			break;
		}
	}

	/// <summary>
	/// 正常的对话.
	/// </summary>
	/// <param name="use">Use.</param>
	private  void makeTalk( DialogFrame use )
	{
		theNameText.text = use.name;
		theInformationText.text = use.information;

		use.picture = !string.IsNullOrEmpty (use.picture) ? use.picture : "noOne";
		Texture2D theTextureIn = SystemValues.LoadResources<Texture2D> ("TalkPicture/" + use.picture); //Resources.Load <Texture2D> ("TalkPicture/" + use.picture);
		Sprite theSprite = Sprite.Create (theTextureIn, new Rect (0, 0, theTextureIn.width, theTextureIn.height), new Vector2 (0, 0));
		theTalkHeadPicture.sprite = theSprite;
	}

	/// <summary>
	/// 开启选项进行选择.
	/// </summary>
	/// <param name="use">Use.</param>
	private void makeSelect(DialogFrame use )
	{
		theInformationText.text = "怎么选择呢？";
		theNameText.text = "";
		use.picture = "noOne";
		Texture2D theTextureIn = SystemValues.LoadResources<Texture2D> ("TalkPicture/" + use.picture);
		UIController.GetInstance ().ShowUI<UITalkSelect> (use.information);
	}

	/// <summary>
	/// 获得经验值，给出消息框
	/// </summary>
	/// <param name="use">Use.</param>
	private void makeOver(DialogFrame use )
	{
		UIController.GetInstance ().ShowUI<PlayerActCanvas> ();
		UIController.GetInstance ().ShowUI<HpBasicPanel> ();
		UIController.GetInstance ().CloseUI<TalkCanvas> ();
		OnEndShow ();
		string [] values = use.information.Split(',');
		if(values.Length >=2)
		{
			SystemValues.thePlayer.GetComponent<Player> ().OnGetLearningValue( (float)XmlConvert.ToDouble (values[0]));
			UIController.GetInstance ().ShowUI<messageBox> (values[1]);
		}
	}

	private void makeMission(DialogFrame use )
	{
		string[] missionNames = use.information.Split (',');
		for (int i = 0; i < missionNames.Length; i++) 
		{
			System.Reflection.Assembly AS = System.Reflection.Assembly.GetExecutingAssembly ();
			MissionBasic theMission = AS.CreateInstance (missionNames[i])  as MissionBasic; 
			SystemValues.thePlayer.GetComponent<Player> ().theMissionPackage.AddNewMission (theMission);
		}
	}

}


class DialogFrame
{
	public string name;
	public string picture;
	public string information;
}