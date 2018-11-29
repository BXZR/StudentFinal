using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileOperater  {

	#region 明文读写
	public void Save (string fileName, string  information)
	{
		FileStream aFile = new FileStream (fileName , FileMode.OpenOrCreate);
		StreamWriter sw = new StreamWriter (aFile);
		sw.Write (information);
		sw.Close();
		sw.Dispose();

	}

	public static string Load(string fileName)
	{
		FileStream aFile = new FileStream(fileName, FileMode.OpenOrCreate );

		StreamReader sw = new StreamReader	(aFile); 
		string information=sw.ReadLine();
		sw.Close ();
		sw.Dispose ();
		return information;
	}
	#endregion

	#region 二进制读写
	public void SaveBinary(string fileName , GameData theData)
	{

		BinaryFormatter binarySaver = new BinaryFormatter ();
		FileStream fileSaver = new FileStream (fileName ,FileMode.Create);
		binarySaver.Serialize (fileSaver , theData);
		fileSaver.Close ();
	}

	public GameData loadBinary(string fileName)
	{
		if (!File.Exists (fileName))
			return null;
		
		BinaryFormatter binaryLoader = new BinaryFormatter ();
		FileStream fileLoader = new FileStream (fileName ,FileMode .Open);
		GameData theLoad =  binaryLoader.Deserialize (fileLoader) as GameData;
		fileLoader.Close ();
		return theLoad;
	}
	#endregion

}


//用这个类保留应存储的东西
[System .Serializable]//标记为可序列化的
public class GameData
{
	public string SceneName;//场景名称
	public float playerPositionX;//玩家坐标
	public float playerPositionY;//玩家坐标
	public float playerPositionZ;//玩家坐标
	public float playerLearn ;//玩家当前经验
	public float playerLearnMax;//玩家经验上限
	public float playerHp;//玩家生命
	public float playerHpMax;//玩家生命上限
	public float playerDamge;//玩家战斗力
	public int playerLv;//玩家等级
	public int [] plotIDs;//当前所有线的剧本进度ID 
	public List<MissionBasic> missions;//当前所有的任务

	//有点坑爹的一点就是不能有构造器
	//否则编译器会报错
}

