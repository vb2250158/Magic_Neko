using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveUtile  {


	/// <summary>
	/// 保存数据
	/// </summary>
	/// <param name="data">Data.</param>
	/// <param name="path">Path.</param>
	public static void Save(string data,string path)
	{
		int fileIndex = path.LastIndexOf ("/");
		//获得文件名
		string fileName = "";
		if (fileIndex!=-1) {

			fileName = path.Substring (fileIndex+1, path.Length - fileIndex-1);
			Debug.Log (fileName);
		} else {
			fileName = path;
		}
			
	
		//检查路径释放缺少/号
		if (path.Substring(0)!="/") {

			path=path.Insert (0,"/");
		}




		if (fileName!=path) {
			Debug.Log (path.Substring (0,fileIndex+1));
			Debug.Log ("创建目录");
			//创建目录
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + path.Substring (0,fileIndex+1));
			directoryInfo.Create();
		}


		Debug.Log (Application.dataPath + path);

	
		//创建文件
		StreamWriter streamWriter = File.CreateText(Application.dataPath + path);
		streamWriter.Write(data);
		streamWriter.Close();
		//   DateTime dataTime = File.GetCreationTime("C:/Users/computer/Desktop/Save/Data.da");
	}

	/// <summary>
	/// 读取数据全部在一起
	/// </summary>
	/// <param name="path">Path.</param>
	public static string Load(string path)
	{
		//检查路径释放缺少/号
		if (path.Substring(0)!="/") {

			path=path.Insert (0,"/");
		}

		if (File.Exists(Application.dataPath + path))
		{

			return File.ReadAllText(Application.dataPath + path);

		}
		else
		{
			return null;
		}


	}
	/// <summary>
	/// 读取数据,拆分每一行
	/// </summary>
	/// <returns>The lines.</returns>
	/// <param name="path">Path.</param>
	public static string[] LoadLines(string path)
	{
     
		//检查路径释放缺少/号
		if (path.Substring(0)!="/") {

			path=path.Insert (0,"/");
		}

		if (File.Exists(Application.dataPath + path))
		{
           // Debug.Log(Application.dataPath + path);
            return File.ReadAllLines(Application.dataPath + path);

		}
		else
		{
			return null;
		}


	}
}
