using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MessageDialogOkCancel : MonoBehaviour {

	[SerializeField]	Button	OkButton;
	[SerializeField]	Button	CancelButton;
	[SerializeField]	Text	MessageText;

	Action OnClickOkButtonCallback = null;
	
	public void Open(string text, Action callback) {
		MessageText.text = text;
		OnClickOkButtonCallback = callback;
		gameObject.SetActive(true);
	}

	public void OnClickCancelButton() {
		gameObject.SetActive(false);
	}
	
	public void OnClickOkButton() {
		gameObject.SetActive(false);
		if (OnClickOkButtonCallback != null) {
			OnClickOkButtonCallback();
		}
	}
}
