using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class TitleScene : SceneBase {

	[SerializeField]	Button	StartCulcButton; 
	[SerializeField]	Button	DataCheckButton; 
	//// Use this for initialization
	//void Start () {
	//
	//}
	//
	//// Update is called once per frame
	//void Update () {
	//
	//}
	
	override public void Initialize() {
		Debug.Log("TitleScene");
		//Debug.Log(Application.persistentDataPath);

		//try {
		//	string path = Application.persistentDataPath + "/data.csv";
		//	StreamWriter sw = new StreamWriter(
		//		path,
		//		false,
		//		Encoding.UTF8);

		//	//TextBox1.Textの内容を書き込む
		//	sw.Write("test");
		//	//閉じる
		//	sw.Close();
		//} catch (IOException e) {
		//	Debug.Log("Open Error");
		//}

		//try {
		//	string path = Application.persistentDataPath + "/data.csv";
		//	StreamReader sr = new StreamReader(
		//		path,
		//		Encoding.UTF8);
		//	string text = sr.ReadToEnd();
		//	Debug.Log(text);
		//	sr.Close();

		//} catch (IOException e) {
		//	Debug.Log("Read Error");
		//}

		DateTime now = DateTime.Now;
		string nowDateString = now.ToString("yyyy_MM_dd_HH_mm_ss");
		Debug.Log(nowDateString);
	}
	 
	public void OnClickStartCulcButton() {
		SceneManager.Instance.ChangeScene(SceneManager.ScoreInputScene);
		//SceneManager.Instance.ChangeScene(SceneManager.AnchorTestScene);
	}
	
	public void OnClickDataCheckButton() {
		SceneManager.Instance.ChangeScene(SceneManager.ResultSelectScene);
	}
}
