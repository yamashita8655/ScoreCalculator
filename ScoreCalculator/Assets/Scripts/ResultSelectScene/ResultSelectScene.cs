using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ResultSelectScene : SceneBase {

	[SerializeField]	Button	TitleButton;
	[SerializeField]	Button	BackButton;
	[SerializeField]	GameObject	ResultSelectListNode;
	[SerializeField]	ScrollRect	SelectScrollView;

	List<GameObject> ResultSelectListNodeList = new List<GameObject>();

	override public void Initialize() {
		Debug.Log("ResultSelectScene");
		string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.csv", System.IO.SearchOption.AllDirectories);

		for (int i = 0; i < files.Length; i++) {
			string fileName = files[i].Replace(Application.persistentDataPath+"\\", "");
			string[] pathSplit = files[i].Split('/');
			Debug.Log(pathSplit[pathSplit.Length-1]);
			//Debug.Log(files[i]);
			//Debug.Log(Application.persistentDataPath);
			//Debug.Log(fileName);
			fileName = fileName.Replace(".csv", "");

			string[] list = fileName.Split('_');
			//string date = string.Format("{0}/{1}/{2} {3}:{4}:{5}", list[0], list[1], list[2], list[3], list[4], list[5]);
			string date = string.Format("{0}/{1}/{2}", list[0], list[1], list[2]);

			GameObject node = Instantiate(ResultSelectListNode);
			node.transform.SetParent(SelectScrollView.content);
			node.transform.position = Vector3.zero;
			node.transform.localScale = Vector3.one;

			ResultSelectListNodeList.Add(node);
			ResultSelectListNode selectNode = node.GetComponent<ResultSelectListNode>();
			
			string src = CsvManager.Instance.LoadCsv(files[i]);
			string[] lines = src.Split('\n');
			string title = "";
			string gameName = "";
			for (int j = 0; j < lines.Length; j++) {
				string[] param = lines[j].Split(',');
				if (param[0] == "TITLE") {
					title = param[1];
				} else if(param[0] == "GAME_NAME") {
					gameName = param[1];
				}
			}
			
			selectNode.Setup(date, title, gameName);
		}		
	}
	 
	public void OnClickTitleButton() {
		SceneManager.Instance.ChangeScene(SceneManager.TitleScene);
	}

	public void OnClickBackButton() {
	}
}
