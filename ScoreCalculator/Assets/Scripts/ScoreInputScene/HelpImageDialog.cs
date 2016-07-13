using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HelpImageDialog : MonoBehaviour {

	[SerializeField] Button CloseButton;
	[SerializeField] HelpImageContainer HelpImageContainer;
	[SerializeField] PageIndicator PageIndicator;
	[SerializeField] Text HelpText;

	List<string> HelpTextList = new List<string>(){
		"1. 追加ボタンでプレイヤーを追加(最大12人まで)\n2. 白領域をタップして名前入力\n3. 人数分入力したら完了",
		"",
		"",
		"",
		""
	};

	public void Open() {
		gameObject.SetActive(true);
		HelpImageContainer.SetViewImage(0);
		PageIndicator.Setup(HelpImageContainer.GetViewImageNum());
	}

	public void OnClickNextButton() {
		HelpImageContainer.ChangeNextViewImage();
	}
	
	public void OnClickPrevButton() {
		HelpImageContainer.ChangePrevViewImage();
	}
	
	public void OnClickCloseButton() {
		gameObject.SetActive(false);
	}
}
