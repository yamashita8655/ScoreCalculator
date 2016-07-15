using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HelpImageContainer : MonoBehaviour {

	[SerializeField] Sprite[] SpriteList;
	[SerializeField] Image ViewImage;
	[SerializeField] Text HelpText;
	
	List<string> HelpTextList = new List<string>(){
		" 1. 追加ボタンでプレイヤーを追加\n   (最大12人まで)\n 2. 白領域をタップして名前入力\n 3. 人数分入力したら完了",
		" 1. 白領域をタップすると\n    スコア入力が表示されます\n 2. 数字を押して数値入力(最大4桁)\n 3. 確定を押して、数値を反映する",
		" 1. 次へを押して、必要なターン数分\n    スコア入力をします(最大999ターン)\n 2. 終わったら、結果を押します",
		" 1. 結果画面になります\n    ご利用ありがとうございましたm(__)m",
		" 1. 操作せずに5分程放置すると\n 自動で広告が再生されます\n ",
		" ご要望、お問い合わせ、苦情は\n ↓こちらまで↓\n mochimoffu@gmail.com\n (お返事にはお時間がかかります)"
	};

	int NowPage = 0;

	public void ChangeNextViewImage() {
		NowPage++;
		if (NowPage >= SpriteList.Length) {
			NowPage = SpriteList.Length - 1;
			return;
		}

		ViewImage.sprite = SpriteList[NowPage];
		HelpText.text = HelpTextList[NowPage];
	}

	public void ChangePrevViewImage() {
		NowPage--;
		if (NowPage < 0) {
			NowPage = 0;
			return;
		}

		ViewImage.sprite = SpriteList[NowPage];
		HelpText.text = HelpTextList[NowPage];
	}

	public void SetViewImage(int page) {
		if (page < 0 || page >= SpriteList.Length) {
			return;
		}
		NowPage = page;
		ViewImage.sprite = SpriteList[NowPage];
		HelpText.text = HelpTextList[NowPage];
	}
	
	public int GetViewImageNum() {
		return SpriteList.Length;
	}
}
