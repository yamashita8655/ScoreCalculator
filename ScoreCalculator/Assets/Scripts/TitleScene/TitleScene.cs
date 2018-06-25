using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class TitleScene : SceneBase {

	[SerializeField]	Button	StartCulcButton; 
	[SerializeField]	Button	DataCheckButton; 
	//// Use this for initialization
	//void Start () {
	//
	//}
	//
	//// Update is called once per frame
	//void Update () {
	//
	//}
	
	override public void Initialize() {
	}
	 
	public void OnClickStartCulcButton() {
		//SceneManager.Instance.ChangeScene(SceneManager.AnchorTestScene);
		GoogleAdmobManager.Instance.ShowAdmob();
		SceneManager.Instance.ChangeScene(SceneManager.ScoreInputScene);
	}

	public void OnClickDataCheckButton() {
		SceneManager.Instance.ChangeScene(SceneManager.ResultSelectScene);
	}
}
