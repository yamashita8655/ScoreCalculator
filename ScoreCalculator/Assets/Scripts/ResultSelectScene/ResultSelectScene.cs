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
	[SerializeField]	GameObject	ResultListNode;// といいつつ、PlayerScoreListNodeの使いまわし
	[SerializeField]	ScrollRect	SelectScrollView;
	[SerializeField]	ScrollRect	ResultScrollView;
	[SerializeField]	ScrollRect	TopScrollView;// とりあえず仮
	[SerializeField]	GameObject	SelectContainer;
	[SerializeField]	GameObject	ResultContainer;
	[SerializeField]	GameObject	TurnLabelListNode;
	[SerializeField]	MessageDialogOkCancel	MessageDialogOkCancelComponent;

	List<GameObject> ResultSelectListNodeList = new List<GameObject>();
	List<GameObject> ResultListNodeList = new List<GameObject>();
	List<GameObject> TurnLabelListNodeList = new List<GameObject>();

	class PlayerData {
		public string Name;
		public List<int> ScoreList;
		public int	TotalScore;
		public int	Rank;

		public PlayerData() {
		}
	}

	override public void Initialize() {
		Debug.Log("ResultSelectScene");

		SwitchContainer(true);

		for (int i = 0; i < ResultSelectListNodeList.Count; i++) {
			ResultSelectListNodeList[i].transform.parent = null;
			Destroy(ResultSelectListNodeList[i]);
		}
		ResultSelectListNodeList.Clear();

		string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.csv", System.IO.SearchOption.AllDirectories);

		for (int i = 0; i < files.Length; i++) {
			string originalFileName = files[i];
			string fileName = files[i].Replace(Application.persistentDataPath+"/", "");
			fileName = fileName.Replace(Application.persistentDataPath+"\\", "");
			string[] pathSplit = files[i].Split('/');
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
			
			selectNode.Setup(originalFileName, date, title, gameName, OnClickOkButtonCallback, OnClickDeleteButtonCallback, src);
		}		
	}
	 
	public void OnClickTitleButton() {
		SceneManager.Instance.ChangeScene(SceneManager.TitleScene);
	}

	public void OnClickBackButton() {
		SwitchContainer(true);
	}
	
	public void OnClickOkButtonCallback(Transform obj, string src) {
		SwitchContainer(false);
		ResultInitialize(src);
	}

	public void OnClickDeleteButtonCallback(string fileName, ResultSelectListNode deleteNode) {
		string str = string.Format("削除しますか？");
		MessageDialogOkCancelComponent.Open(str, () => {
			DeleteSaveFile(fileName);
			RemoveResultSelectListNode(deleteNode);
		});
	}

	private void DeleteSaveFile(string fileName) {
		System.IO.File.Delete(fileName);
	}

	private void RemoveResultSelectListNode(ResultSelectListNode deleteNode) {
		for (int i = 0; i < ResultSelectListNodeList.Count; i++) {
			GameObject obj = ResultSelectListNodeList[i];
			ResultSelectListNode node = obj.GetComponent<ResultSelectListNode>();
			if (node == deleteNode) {
				ResultSelectListNodeList.Remove(obj);
				obj.transform.SetParent(null);
				Destroy(obj);
				break;
			}
		}
	}

	private void SwitchContainer(bool activeSelect) {
		if (activeSelect == true) {
			SelectContainer.SetActive(true);
			ResultContainer.SetActive(false);
		} else {
			SelectContainer.SetActive(false);
			ResultContainer.SetActive(true);
		}
	}

	private void ResultInitialize(string src) {
		char[] lineSplitRule = {'\n'};
		string[] lineStringList = src.Split(lineSplitRule);

		int playerNum = 0;
		int turnNum = 0;
		char[] splitRule = {','};

		List<PlayerData> playerDataList = new List<PlayerData>();

		for (int i = 0; i < lineStringList.Length; i++) {
			string[] list = lineStringList[i].Split(splitRule);
			if (list[0] == "TURN") {
				turnNum = int.Parse(list[1]);
			} else if (list[0] == "NAME") {
				List<int> scoreList = new List<int>();
				int index = 1;
				PlayerData pData = new PlayerData();
				pData.Name = list[index];
				index++;
				for (int j = 0; j < turnNum; j++) {
					scoreList.Add(int.Parse(list[index]));
					index++;
				}
				pData.ScoreList = scoreList;
				pData.TotalScore = int.Parse(list[index]);
				index++;
				pData.Rank = int.Parse(list[index]);
				playerDataList.Add(pData);
			}
		}
		
		for (int i = 0; i < ResultListNodeList.Count; i++) {
			ResultListNodeList[i].transform.SetParent(null);
			Destroy(ResultListNodeList[i]);
		}
		ResultListNodeList.Clear();
		
		for (int i = 0; i < TurnLabelListNodeList.Count; i++) {
			TurnLabelListNodeList[i].transform.SetParent(null);
			Destroy(TurnLabelListNodeList[i]);
		}
		TurnLabelListNodeList.Clear();

		for (int i = 0; i < playerDataList.Count; i++) {
			GameObject node = Instantiate(ResultListNode);
			node.transform.SetParent(ResultScrollView.content);
			node.transform.position = Vector3.zero;
			node.transform.localScale = Vector3.one;
			PlayerScoreListNode accessNode = node.GetComponent<PlayerScoreListNode>();
			accessNode.Setup(null, i, OnScrollValueChange, null, null, false);
			
			accessNode.SetName(playerDataList[i].Name);
			accessNode.SetTotalScoreText(playerDataList[i].TotalScore.ToString());
			accessNode.SetRankText(playerDataList[i].Rank.ToString());
			if (playerDataList[i].Rank == 1) {
				accessNode.SetRankTopIconEnable(true);
			} else {
				accessNode.SetRankTopIconEnable(false);
			}
			accessNode.SetEnableInputField(false);
			

			for (int j = 0; j < playerDataList[i].ScoreList.Count; j++) {
				accessNode.AddScoreListNode(false);
				accessNode.SetScoreListScoreText(playerDataList[i].ScoreList[j].ToString());
			}

			ResultListNodeList.Add(node);
		}

		for (int i = 0; i < turnNum; i++) {
			AddTurnLabelListNode();
		}
			
		OnScrollValueChange(Vector2.zero);
	}
	
	public void OnScrollValueChange(Vector2 pos) {
		TopScrollView.normalizedPosition = pos;
		for (int i = 0; i < ResultListNodeList.Count; i++) {
			PlayerScoreListNode node = ResultListNodeList[i].GetComponent<PlayerScoreListNode>();
			node.UpdateScrollValue(pos);
		}
	}
	
	private void AddTurnLabelListNode() {
		GameObject node = Instantiate(TurnLabelListNode);
		node.transform.SetParent(TopScrollView.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		TurnLabelListNodeList.Add(node);
		int count = TurnLabelListNodeList.Count;
		TurnListNode turnNode = node.GetComponent<TurnListNode>();
		turnNode.Setup();
		turnNode.SetTurnText("Turn" + count.ToString());
	}
}

