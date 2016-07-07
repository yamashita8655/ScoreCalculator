using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Indicator : MonoBehaviour {

	[SerializeField] Image OnImage;
	[SerializeField] Image OffImage;
	
	bool IsToggleOn = true;

	public void ToggleImage() {
		IsToggleOn = !IsToggleOn;
		UpdateToggleImage();
	}
	
	public void SetToggleFlag(bool toggleOn) {
		IsToggleOn = toggleOn;
		UpdateToggleImage();
	}

	private void UpdateToggleImage() {
		if (IsToggleOn == true) {
			OnImage.enabled = true;
			OffImage.enabled = false;
		} else {
			OnImage.enabled = false;
			OffImage.enabled = true;
		}
	}
}
