using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageDialog : MonoBehaviour {

	[SerializeField]	Button	CloseButton;
	[SerializeField]	Text	MessageText;
	
	public void Open(string text) {
		MessageText.text = text;
		gameObject.SetActive(true);
	}

	public void OnClickCloseButton() {
		gameObject.SetActive(false);
	}
}
