using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpImageDialog : MonoBehaviour {

	[SerializeField] Button CloseButton;
	[SerializeField] HelpImageContainer HelpImageContainer;
	[SerializeField] PageIndicator PageIndicator;


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
