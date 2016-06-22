﻿using UnityEngine;
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

	List<GameObject>	ScoreListNodeList = new List<GameObject>();
	ScoreListNode		NowSelectScoreListNode = null;

	public void Setup(Action<List<string>> callback) {
		ScoreInputSceneEndEditCallback = callback;
    }

	public void SetupScoreListNode(Action<List<string>> callback) {
		NowSelectScoreListNode.Setup(callback);
	}

    public void SetName(string name) {
        NameInputField.text = name;
    }
    
	public string GetName() {
        return NameInputField.text;
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
}
