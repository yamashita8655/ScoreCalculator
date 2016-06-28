using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeaderContainer : MonoBehaviour {

	[SerializeField]	Button	TitleButton; 
	[SerializeField]	Button	RuleConfigButton; 
	[SerializeField]	Text	RuleConfigText; 

//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
	
	public void Init() {
	}
	
	public void SetRuleConfigText(string text) {
		RuleConfigText.text = text;
	}
	
}
