using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class ResultSelectScene : SceneBase {

	[SerializeField]	Button	TitleButton;
	[SerializeField]	Button	BackButton;

	override public void Initialize() {
		Debug.Log("ResultSelectScene");
	}
	 
	public void OnClickTitleButton() {
		SceneManager.Instance.ChangeScene(SceneManager.TitleScene);
	}

	public void OnClickBackButton() {
	}
}
