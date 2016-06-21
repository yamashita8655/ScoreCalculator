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

	Action<List<string>> ScoreInputSceneEndEditCallback = null;

	public void Setup(Action<List<string>> callback) {
		ScoreInputSceneEndEditCallback = callback;
    }

    public void SetName(string name) {
        NameInputField.text = name;
    }

	public void ActivateInputField() {
		NameInputField.ActivateInputField();
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
}
