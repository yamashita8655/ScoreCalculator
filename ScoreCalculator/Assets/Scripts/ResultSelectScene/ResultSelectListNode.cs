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
	Action<string, ResultSelectListNode> DeleteButtonCallback = null;
	string ResultSource = "";
	string FileName = "";

	public void Setup(string fileName, string date, string title, string gameName, Action<Transform, string> okButtonCallback, Action<string, ResultSelectListNode> deleteButtonCallback, string resultSource) {
		DateText.text = date;
		TitleText.text = title;
		GameNameText.text = gameName;
		OkButtonCallback = okButtonCallback;
		DeleteButtonCallback = deleteButtonCallback;
		ResultSource = resultSource;
		FileName = fileName;
	}

	public void OnClickViewButton() {
		OkButtonCallback(this.transform, ResultSource);
	}

	public void OnClickDeleteButton() {
		DeleteButtonCallback(FileName, this);
	}
}
