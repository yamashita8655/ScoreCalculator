using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScoreListNode : MonoBehaviour {
	[SerializeField] InputField NameInputField;
	[SerializeField] Text TotalText;
	[SerializeField] Text RankText;
	[SerializeField] ScrollRect ScoreScrollRext;
	[SerializeField] GameObject	ScoreListNodeObject;

	Action<List<string>> ScoreInputSceneEndEditCallback = null;
	Action<List<string>, int> ScoreInputEndEditCallback = null;

	List<GameObject>	ScoreListNodeList = new List<GameObject>();
	ScoreListNode		NowSelectScoreListNode = null;

	int					Number = 0;

	public void Setup(Action<List<string>> callback, Action<List<string>, int> scoreInputEndCallback, int number) {
		ScoreInputSceneEndEditCallback = callback;
		ScoreInputEndEditCallback = scoreInputEndCallback;
		Number = number;
		InitTotalScoreText();
		InitRankingText();
	}

	public int GetNumber() {
		return Number;
	}

	public List<GameObject> GetScoreListNodeList() {
		return ScoreListNodeList;
	}

	public void SetupScoreListNode() {
		NowSelectScoreListNode.Setup(ScoreInputFieldEndEditCallback);
	}

	public void SetName(string name) {
		NameInputField.text = name;
	}

	public void SetScoreText(string score) {
		NowSelectScoreListNode.SetScoreText(score);
	}
	
	public void SetRankText(string rank) {
		RankText.text = rank;
	}
	
	public string GetName() {
		return NameInputField.text;
	}
	
	public string GetScoreText() {
		return NowSelectScoreListNode.GetScoreText();
	}

	public void ActivateInputField() {
		NameInputField.ActivateInputField();
	}
	
	public void ActivateScoreInputField() {
		NowSelectScoreListNode.ActivateInputField();
	}

	public void AddScoreListNode() {
		AddScoreListNodeObject();
	}

	// Update is called once per frame
	void Update () {
	}

	public void OnEndEditNameInputField() {
		string inputString = NameInputField.text.Trim();
		if (string.IsNullOrEmpty (inputString) == true) {
			return;
		}

		char[] splitRule = {',', '、'};
		string[] split = inputString.Split(splitRule);
		List<string> stringList = new List<string>();

		for (int i = 0; i < split.Length; i++) {
			if (string.IsNullOrEmpty (split[i]) == false) {
				stringList.Add(split[i]);
			}
		}

		NameInputField.text = stringList[0];

		if (ScoreInputSceneEndEditCallback != null) {
			ScoreInputSceneEndEditCallback(stringList);
		}
	}

	public void SetEnableInputField(bool enable) {
		NameInputField.enabled = enable;
	}
	
	public void SetEnableScoreInputField(bool enable) {
		NowSelectScoreListNode.SetEnableInputField(enable);
	}

	private void AddScoreListNodeObject() {
		GameObject node = Instantiate(ScoreListNodeObject);
		node.transform.SetParent(ScoreScrollRext.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		ScoreListNodeList.Add(node);
		NowSelectScoreListNode = node.GetComponent<ScoreListNode>();
		NowSelectScoreListNode.SetEnableInputField(false);
//		NowSelectScoreListNode.Setup(PlayerNameInputEndEditCallback);
	}

	public int RemoveScoreListNodeObject() {
		int lastIndex = ScoreListNodeList.Count - 1;
		GameObject lastObj = ScoreListNodeList[lastIndex];
		Destroy(lastObj);
		ScoreListNodeList.RemoveAt(lastIndex);
		NowSelectScoreListNode = null;

		int count = ScoreListNodeList.Count;

		if (count != 0) {
			GameObject newLastObj = ScoreListNodeList[ScoreListNodeList.Count-1];
			NowSelectScoreListNode = newLastObj.GetComponent<ScoreListNode>();
		}

		return count;
	}
	
	public void RemoveAllScoreListNodeObject() {
		for (int i = 0; i < ScoreListNodeList.Count; i++) {
			GameObject obj = ScoreListNodeList[i];
			Destroy(obj);
		}
		ScoreListNodeList.Clear();
		NowSelectScoreListNode = null;
	}

	private void ScoreInputFieldEndEditCallback(List<string> inputStrings) {
		ScoreInputEndEditCallback(inputStrings, Number);
	}

	public void UpdateTotalScoreText() {
		int totalScore = 0;
		for (int i = 0; i < ScoreListNodeList.Count; i++) {
			GameObject obj = ScoreListNodeList[i];
			ScoreListNode node = obj.GetComponent<ScoreListNode>();
			int score = int.Parse(node.GetScoreText());
			totalScore += score;
		}

		TotalText.text = totalScore.ToString();
	}

	public void InitTotalScoreText() {
		TotalText.text = "0";
	}
	
	public string GetTotalScoreText() {
		return TotalText.text;
	}
	
	public void InitRankingText() {
		RankText.text = "0";
	}
}
