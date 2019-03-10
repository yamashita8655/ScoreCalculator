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
		SceneManager.Instance.ChangeScene(SceneManager.ScoreInputScene);
	}

	public void OnClickDataCheckButton() {
        // ここで、Admob初期化して、セーブデータから戻ってくるときに、表示するようにする
        GoogleAdmobManager.Instance.Initialize();
        SceneManager.Instance.ChangeScene(SceneManager.ResultSelectScene);
	}
}
