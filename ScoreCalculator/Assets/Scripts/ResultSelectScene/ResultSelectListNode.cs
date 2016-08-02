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

	public void Setup(string date, string title, string gameName) {
		DateText.text = date;
		TitleText.text = title;
		GameNameText.text = gameName;
	}

	public void OnClickViewButton() {
	}
}
