using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScoreListNode : MonoBehaviour {
    [SerializeField] InputField ScoreInputField;
    [SerializeField] Text ScoreInputText;
    [SerializeField] EventTrigger ScoreInputEventTrigger;

	Action<List<string>> ScoreInputSceneEndEditCallback = null;
	
	Action<Text> OpenScoreInputer = null;

	public void Setup(Action<List<string>> scoreInputEndEditCallback, Action<Text> openScoreInputer) {
		ScoreInputSceneEndEditCallback = scoreInputEndEditCallback;
		OpenScoreInputer = openScoreInputer;
    }
	
	public void ActivateInputField() {
		ScoreInputField.ActivateInputField();
	}
	
	public void SetScoreText(string text) {
		if (string.IsNullOrEmpty(text)) {
			return;
		}
		ScoreInputField.text = text;
    }
	
	public string GetScoreText() {
		return ScoreInputText.text;
    }

	// Update is called once per frame
	void Update () {
	}
	
	public void OnEndEditScoreInputField() {
		string inputString = ScoreInputField.text.Trim();
		if (string.IsNullOrEmpty(inputString) == true) {
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

		ScoreInputField.text = stringList[0];

		if (ScoreInputSceneEndEditCallback != null) {
			ScoreInputSceneEndEditCallback(stringList);
		}
	}

	public void SetEnableInputField(bool enable) {
		ScoreInputField.enabled = enable;
	}

	public void OnClickClickableText() {
		OpenScoreInputer(ScoreInputText);
	}

	public void SetClickableTextEnable(bool enable) {
		ScoreInputEventTrigger.enabled = enable;
	}
}
