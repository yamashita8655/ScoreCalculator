using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpImageContainer : MonoBehaviour {

	[SerializeField] Sprite[] SpriteList;
	[SerializeField] Image ViewImage;

	int NowPage = 0;

	public void ChangeNextViewImage() {
		NowPage++;
		if (NowPage >= SpriteList.Length) {
			NowPage = SpriteList.Length - 1;
			return;
		}

		ViewImage.sprite = SpriteList[NowPage];
	}

	public void ChangePrevViewImage() {
		NowPage--;
		if (NowPage < 0) {
			NowPage = 0;
			return;
		}

		ViewImage.sprite = SpriteList[NowPage];
	}

	public void SetViewImage(int page) {
		if (page < 0 || page >= SpriteList.Length) {
			return;
		}
		NowPage = page;
		ViewImage.sprite = SpriteList[NowPage];
	}
	
	public int GetViewImageNum() {
		return SpriteList.Length;
	}
}
