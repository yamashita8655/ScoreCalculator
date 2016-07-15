using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScoreListNode : MonoBehaviour {
	[SerializeField] InputField NameInputField;
	[SerializeField] Text TotalText;
	[SerializeField] Text RankText;
	[SerializeField] ScrollRect ScoreScrollRect;
	[SerializeField] GameObject	ScoreListNodeObject;
	[SerializeField] Image	RankTopImage;

	Action<List<string>> ScoreInputSceneEndEditCallback = null;

	List<GameObject>	ScoreListNodeList = new List<GameObject>();
	ScoreListNode		NowSelectScoreListNode = null;

	int					Number = 0;
	
	Action<Vector2> ScrollValueChangeCallback = null;
	
	Action<Text> OpenScoreInputerCallback = null;

	public void Setup(Action<List<string>> callback, int number, Action<Vector2> scrollCallback, Action<Text> openScoreInputerCallback) {
		ScoreInputSceneEndEditCallback = callback;
		ScrollValueChangeCallback = scrollCallback;
		OpenScoreInputerCallback = openScoreInputerCallback;
		Number = number;
		InitTotalScoreText();
		InitRankingText();
		SetRankTopIconEnable(false);
	}

	public int GetNumber() {
		return Number;
	}

	public List<GameObject> GetScoreListNodeList() {
		return ScoreListNodeList;
	}

	public void SetupScoreListNode() {
		NowSelectScoreListNode.Setup(OpenScoreInputerCallback);
	}
	
	public void SetScoreListScoreText(string score) {
		NowSelectScoreListNode.SetScoreText(score);
	}

	public void SetName(string name) {
		NameInputField.text = name;
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

	public void AddScoreListNode(bool enabled) {
		AddScoreListNodeObject(enabled);
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
	
	public void SetClickableTextEnable(bool enable) {
		NowSelectScoreListNode.SetClickableTextEnable(enable);
	}

	private void AddScoreListNodeObject(bool enabled) {
		GameObject node = Instantiate(ScoreListNodeObject);
		node.transform.SetParent(ScoreScrollRect.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		ScoreListNodeList.Add(node);
		NowSelectScoreListNode = node.GetComponent<ScoreListNode>();
		NowSelectScoreListNode.SetClickableTextEnable(enabled);
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

	public void UpdateTotalScoreText() {
		int totalScore = 0;
		for (int i = 0; i < ScoreListNodeList.Count; i++) {
			GameObject obj = ScoreListNodeList[i];
			ScoreListNode node = obj.GetComponent<ScoreListNode>();
			int score = 0;
			if (int.TryParse(node.GetScoreText(), out score)) {
				totalScore += score;
			}
		}

		TotalText.text = totalScore.ToString();
	}
	
	public void SetTotalScoreText(string score) {
		TotalText.text = score;
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

	public string GetRankingText() {
		return RankText.text;
	}
	
	public void SetRankingText(string rank) {
		RankText.text = rank;
	}

	public void UpdateScrollValue(Vector2 pos) {
		ScoreScrollRect.normalizedPosition = pos;
	}
	
	public void OnScrollValueChange(Vector2 pos) {
		ScrollValueChangeCallback(pos);
	}

	public void SetRankTopIconEnable(bool enable) {
		RankTopImage.enabled = enable;
	}
	
	public void UpdateRankTopIconEnable() {
		if (RankText.text == "1") {
			RankTopImage.enabled = true;
		} else {
			RankTopImage.enabled = false;
		}
	}
}
