using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SaveDialog : MonoBehaviour {

	[SerializeField]	InputField	TitleInput;
	[SerializeField]	InputField	GameNameInput;
	private	Action<string, string>	SaveGameDataCallback;
	
	public void Open(Action<string, string> callback) {
		TitleInput.text = "";
		GameNameInput.text = "";
		SaveGameDataCallback = callback;
		gameObject.SetActive(true);
	}
	
	public void OnClickSaveButton() {
		SaveGameDataCallback(TitleInput.text, GameNameInput.text);
		gameObject.SetActive(false);
	}

	public void OnClickBackButton() {
		gameObject.SetActive(false);
	}
}
