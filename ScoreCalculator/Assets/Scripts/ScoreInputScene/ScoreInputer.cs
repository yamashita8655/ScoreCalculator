using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreInputer : MonoBehaviour {

	[SerializeField] Text InputText;

	Text OutputText;

	public void Open(Text outputText) {
		OutputText = outputText;
		gameObject.SetActive(true);
	}

	public void OnClickInputerButton(int type) {
		string text = InputText.text;
		switch (type) {
		case 0:
			if (text.StartsWith("-") == true && text.Length == 1) {
				break;
			}

			if (text.StartsWith("0") == false) {
				text += "0";
			}
			break;
		case 1:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "1");
			} else {
				text += "1";
			}
				break;
		case 2:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "2");
			} else {
				text += "2";
			}
				break;
		case 3:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "3");
			} else {
				text += "3";
			}
				break;
		case 4:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "4");
			} else {
				text += "4";
			}
				break;
		case 5:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "5");
			} else {
				text += "5";
			}
				break;
		case 6:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "6");
			} else {
				text += "6";
			}
				break;
		case 7:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "7");
			} else {
				text += "7";
			}
				break;
		case 8:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "8");
			} else {
				text += "8";
			}
				break;
		case 9:
			if (text.StartsWith("0") == true) {
				text = text.Replace("0", "9");
			} else {
				text += "9";
			}
				break;
		case 10:// プラマイボタン
			if (text.StartsWith("0") == true) {
				break;
			}
	
			if (text.StartsWith("-") == true) {
				text = text.Remove(0, 1);
			} else {
				text = text.Insert(0, "-");
			}
				break;
		case 11:// 決定ボタン
			StartCoroutine(Decide());
			return;
		case 12:// 初期化
				text = "";
				break;
		}

		if (text.StartsWith("-") == true) {
			if (text.Length <= 5) {
				InputText.text = text;
			}
		} else {
			if (text.Length <= 4) {
				InputText.text = text;
			}
		}
	}

	private IEnumerator Decide() {
		if (string.IsNullOrEmpty(InputText.text) != true) {
			OutputText.text = InputText.text;
		}

		InputText.text = "";
		yield return null;
		gameObject.SetActive(false);
	}
}
