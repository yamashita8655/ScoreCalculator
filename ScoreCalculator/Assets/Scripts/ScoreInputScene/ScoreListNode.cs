using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScoreListNode : MonoBehaviour {
    [SerializeField] Text ScoreInputText;
    [SerializeField] EventTrigger ScoreInputEventTrigger;

	Action<Text> OpenScoreInputer = null;

	public void Setup(Action<Text> openScoreInputer) {
		OpenScoreInputer = openScoreInputer;
    }
	
	public string GetScoreText() {
		return ScoreInputText.text;
    }
	
	public void SetScoreText(string score) {
		ScoreInputText.text = score;
    }

	// Update is called once per frame
	void Update () {
	}
	
	public void OnClickClickableText() {
		OpenScoreInputer(ScoreInputText);
	}

	public void SetClickableTextEnable(bool enable) {
		ScoreInputEventTrigger.enabled = enable;
	}
}
