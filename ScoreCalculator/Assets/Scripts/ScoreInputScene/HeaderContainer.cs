using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeaderContainer : MonoBehaviour {

	[SerializeField]	Button	TitleButton; 
	[SerializeField]	Button	RuleConfigButton; 

//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
	
	public void OnClickTitleButton() {
		SceneManager.Instance.ChangeScene(SceneManager.TitleScene);
	}
	
	public void OnClickRuleConfigButton() {
	}
}
