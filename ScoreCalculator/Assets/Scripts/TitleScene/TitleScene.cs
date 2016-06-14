using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		Debug.Log("TitleScene");
	}

	public void OnClickStartCulcButton() {
		SceneManager.Instance.ChangeScene(SceneManager.ScoreInputScene);
	}
	
	public void OnClickDataCheckButton() {
	}
}
