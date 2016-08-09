using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResultSelectListNode : MonoBehaviour {
	[SerializeField] Text DateText;
	[SerializeField] Text TitleText;
	[SerializeField] Text GameNameText;
	[SerializeField] Button ViewButton;

	Action<Transform, string> OkButtonCallback = null;
	string ResultSource = "";

	public void Setup(string date, string title, string gameName, Action<Transform, string> okButtonCallback, string resultSource) {
		DateText.text = date;
		TitleText.text = title;
		GameNameText.text = gameName;
		OkButtonCallback = okButtonCallback;
		ResultSource = resultSource;
	}

	public void OnClickViewButton() {
		OkButtonCallback(this.transform, ResultSource);
	}
}
