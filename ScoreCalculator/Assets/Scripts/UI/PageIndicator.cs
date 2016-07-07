using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PageIndicator : MonoBehaviour {

	[SerializeField] Button LeftButton;
	[SerializeField] Button RightButton;
	[SerializeField] GameObject IndicatorContainer;
	[SerializeField] GameObject IndicatorObject;

	List<GameObject> IndicatorList = new List<GameObject>();
	int nowPage = 0;

	public void Setup(int indicatorCount) {
		if (IndicatorList.Count == 0) {
			for (int i = 0; i < indicatorCount; i++) {
				GameObject indicator = Instantiate(IndicatorObject);
				indicator.transform.SetParent(IndicatorContainer.transform);
				indicator.transform.localPosition = Vector3.zero;
				indicator.transform.localScale = Vector3.one;
				IndicatorList.Add(indicator);
			}
		}

		nowPage = 0;
		UpdateIndicator();
		UpdatePosition();
	}
	
	void UpdatePosition() {
		int count = IndicatorList.Count;
		float width = IndicatorObject.GetComponent<RectTransform>().rect.width;
		float widthOffset = width*count / 2;

		for (int i = 0; i < count; i++) {
			IndicatorList[i].transform.localPosition = new Vector3(-widthOffset + i*width + width/2, 0f, 0f);
		}
	}

	void UpdateIndicator() {
		for (int i = 0; i < IndicatorList.Count; i++) {
			Indicator indicator = IndicatorList[i].GetComponent<Indicator>();
			if (i == nowPage) {
				indicator.SetToggleFlag(true);
			} else {
				indicator.SetToggleFlag(false);
			}
		}
	}

	public void OnClickLeftButton() {
		nowPage--;
		if (nowPage < 0) {
			nowPage = 0;
			return;
		}
		
		UpdateIndicator();
	}
	
	public void OnClickRightButton() {
		nowPage++;
		if (nowPage >= IndicatorList.Count) {
			nowPage = IndicatorList.Count - 1;
			return;
		}
		
		UpdateIndicator();
	}
}
