using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreInputScene : SceneBase {

	[SerializeField]	GameObject	HeaderContainer;
	[SerializeField]	GameObject	PlayerNameInputContainer;
	[SerializeField]	GameObject	ScoreInputContainer;
	[SerializeField]	GameObject	ResultContainer;
	[SerializeField]	GameObject	PlayerScoreListNode;
	[SerializeField]	GameObject	TurnLabelListNode;
	[SerializeField]	GameObject	TopListNode;
	[SerializeField]	ScrollRect	BaseScrollView;
	[SerializeField]	ScrollRect	TopScrollView;// とりあえず仮
	[SerializeField]	GameObject	ScoreInputer;
	[SerializeField]	GameObject	HelpImageDialog;
	[SerializeField]	GameObject	MessageDialogObject;
	[SerializeField]	MessageDialog	MessageDialogComponent;
	[SerializeField]	GameObject	MessageDialogOkCancelObject;
	[SerializeField]	MessageDialogOkCancel	MessageDialogOkCancelComponent;
	[SerializeField]	GameObject	SaveDialogObject;
	[SerializeField]	SaveDialog	SaveDialogComponent;

	// 途中セーブに必要な情報
	// 参加者人数
		// 参加者の名前
		// 参加者のスコア
	// ターン数

	/*
		PLAYER_NUM,5
		TURN,3
		NAME,yasuo,5,,5
		NAME,yasuo
		NAME,yasuo
		NAME,yasuo
		NAME,yasuo
	*/

	
	private int	PlayerCountMax = 12;
	private int	TurnCountMax = 999;

	List<GameObject>	PlayerScoreListNodeList = new List<GameObject>();
	List<GameObject>	TurnLabelListNodeList = new List<GameObject>();

	enum ToggleType {
		PlayerNameInput,
		ScoreInput,
		Result,
	}

	enum State {
		PlayerNameInputInit,
		PlayerNameInputUpdate,
		
		ScoreInputInit,
		ScoreInputUpdate,
		
		ResultInit,
		ResultUpdate,
	}
	
	enum RuleConfigState {
		DescendingOrder = 0,
		AscendingOrder,
	}

	State NowState = State.PlayerNameInputInit;
	RuleConfigState NowRuleConfing = RuleConfigState.DescendingOrder;

	private string InprogressDataKey		= "INPROGRESSDATA";
	private string InprogressNameKey		= "NAME";
	private string InprogressTurnKey 		= "TURN";
	private string InprogressPlayerNumKey	= "PLAYER_NUM";
	private string InprogressTitleKey		= "TITLE";
	private string InprogressGameNameKey	= "GAME_NAME";
	private string InprogressDateKey		= "DATE";

	float ShowAdsCounter = 0.0f;
	float ShowAdsTime = 300.0f;
//	float ShowAdsTime = 5.0f;
	bool ShowAdsFlag = false;

	//[SerializeField]	Button	StartCulcButton; 
	//[SerializeField]	Button	DataCheckButton; 
	
	//// Use this for initialization
	//void Start () {
	//
	//}

	override public void Initialize() {
		NowState = State.PlayerNameInputInit;

		HeaderContainer.SetActive(true); 
		PlayerNameInputContainer.SetActive(false); 
		ScoreInputContainer.SetActive(false); 
		ResultContainer.SetActive(false); 

		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			PlayerScoreListNodeList[i].transform.parent = null;
			Destroy(PlayerScoreListNodeList[i]);
		}
		PlayerScoreListNodeList.Clear();
		
		for (int i = 0; i < TurnLabelListNodeList.Count; i++) {
			TurnLabelListNodeList[i].transform.parent = null;
			Destroy(TurnLabelListNodeList[i]);
		}
		TurnLabelListNodeList.Clear();
		
		UpdateRuleConfigString();

		ScoreInputer.SetActive(false);
		HelpImageDialog.SetActive(false);
		MessageDialogObject.SetActive(false);
		MessageDialogOkCancelObject.SetActive(false);
		SaveDialogObject.SetActive(false);
		
		CheckInprogressData();
	}

	// Update is called once per frame
	void Update () {
		if (NowState == State.PlayerNameInputInit) {
			PlayerNameInputInit();
		} else if (NowState == State.PlayerNameInputUpdate) {
			PlayerNameInputUpdate();
		} else if (NowState == State.ScoreInputInit) {
			ScoreInputInit();
		} else if (NowState == State.ScoreInputUpdate) {
			ScoreInputUpdate();
		} else if (NowState == State.ResultInit) {
			ResultInit();
		} else if (NowState == State.ResultUpdate) {
			ResultUpdate();
		}
	}

	void PlayerNameInputInit() {
		ToggleContainer(ToggleType.PlayerNameInput);
		NowState = State.PlayerNameInputUpdate;

		InitTotalScore();
		InitRanking();

		SetRemoveButtonActive(false);
	}

	private PlayerScoreListNode GetNowPlayerScoreListNode() {
		if (PlayerScoreListNodeList.Count == 0) {
			return null;
		}


		GameObject obj = PlayerScoreListNodeList[PlayerScoreListNodeList.Count-1];
		PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
		return node;
	}

	void PlayerNameInputUpdate() {
	}
	
	void ScoreInputInit() {
		ToggleContainer(ToggleType.ScoreInput);
		NowState = State.ScoreInputUpdate;

		if (IsScoreListCountZero() == true) {
			AddScoreInputListNode();
			AddTurnLabelListNode();
		} else {
			SetClickableTextListEnable(true);
			OnScrollValueChange(Vector2.one);
		}

		InitRankTopIconEnable();
		SetRemoveButtonActive(true);
	}

	private void InitRankTopIconEnable() {
		// スコア入力ノードを、プレイヤー数分一度に追加する
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.SetRankTopIconEnable(false);
		}
	}

	private void AddScoreInputListNode() {
		// スコア入力ノードを、プレイヤー数分一度に追加する
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.AddScoreListNode(true);
			node.SetupScoreListNode();
		}
	}
	
	void ScoreInputUpdate() {
#if false
		if (ShowAdsFlag == false) {
		} else {
			if (NowState == State.ScoreInputUpdate) {
				ShowAdsCounter += Time.deltaTime;
				if (ShowAdsCounter > ShowAdsTime) {
					UnityAdsManager.Instance.ShowAd();
					ShowAdsCounter = 0.0f;
					ShowAdsFlag = false;
				}
			}
		}
#endif
	}
	
	void ResultInit() {
		ToggleContainer(ToggleType.Result);
		NowState = State.ResultUpdate;
		
		UpdateRankTopIconEnable();
		SetRemoveButtonActive(false);
	}

	void UpdateRankTopIconEnable() {
		// ランキングアイコンの表示更新
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.UpdateRankTopIconEnable();
		}
	}
	
	void ResultUpdate() {
	}

	void ToggleContainer(ToggleType toggle) {
		PlayerNameInputContainer.SetActive(false);
		ScoreInputContainer.SetActive(false);
		ResultContainer.SetActive(false);
		
		if (toggle == ToggleType.PlayerNameInput) {
			PlayerNameInputContainer.SetActive(true);
		} else if (toggle == ToggleType.ScoreInput) {
			ScoreInputContainer.SetActive(true);
		} else if (toggle == ToggleType.Result) {
			ResultContainer.SetActive(true);
		}
	}
	
	// HeaderContainerのマウスイベント
	public void OnClickHeaderTitleButton() {
		ResetShowAdsCounter();
		string str = string.Format("タイトルに戻りますか？\n\n※注意※\n入力情報は削除されます");
		MessageDialogOkCancelComponent.Open(str, () => {
			ClearInprogressData();
			SceneManager.Instance.ChangeScene(SceneManager.TitleScene);
		});
	}
	
	public void OnClickHeaderRuleConfigButton() {
		ResetShowAdsCounter();
		// ここの条件設定などは、後でダイアログか何かで選択した結果に変更すると思う
		if (NowRuleConfing == RuleConfigState.DescendingOrder) {
			NowRuleConfing = RuleConfigState.AscendingOrder;
		} else {
			NowRuleConfing = RuleConfigState.DescendingOrder;
		}
		UpdateRuleConfigString();
		
		if (NowState != State.PlayerNameInputUpdate) {
			UpdateRanking();
		}

		if (NowState == State.ResultUpdate) {
			UpdateRankTopIconEnable();
		}
	}
	
	public void OnClickHeaderHelpButton() {
		ResetShowAdsCounter();
		HelpImageDialog dialog = HelpImageDialog.GetComponent<HelpImageDialog>();
		dialog.Open();
	}

	// PlayerNameInputContainerのマウスイベント
	public void OnClickPlayerNameInputDeleteButton() {
		ResetShowAdsCounter();
		if (PlayerScoreListNodeList.Count <= 0) {
			return;
		}
		
		GameObject nowSelectObj = PlayerScoreListNodeList[PlayerScoreListNodeList.Count-1];
		PlayerScoreListNodeList.RemoveAt(PlayerScoreListNodeList.Count-1);
		nowSelectObj.transform.SetParent(null);
		Destroy(nowSelectObj);
	}
	
	public void OnClickPlayerNameInputAddButton() {
		ResetShowAdsCounter();
		if (PlayerScoreListNodeList.Count < PlayerCountMax) {
			AddPlayerScoreListNode();
		} else {
			string str = string.Format("これ以上人数は増やせません。\n(最大{0}人まで)", PlayerCountMax);
			MessageDialogComponent.Open(str);
		}
	}
	
	public void OnClickPlayerNameInputInputEndButton() {
		ResetShowAdsCounter();

		List<GameObject> deleteList = new List<GameObject>();
		
		// 名前空文字の要素を削除する
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			if (string.IsNullOrEmpty(node.GetName())) {
				deleteList.Add(obj);
				obj.transform.SetParent(null);
			}
		}

		for (int i = 0; i < deleteList.Count; i++) {
			PlayerScoreListNodeList.Remove(deleteList[i]);
			Destroy(deleteList[i]);
		}

		if (PlayerScoreListNodeList.Count > 0) {
			SaveInprogressData();
			NowState = State.ScoreInputInit;
		} else {
			MessageDialogComponent.Open("プレイヤーは最低一人以上\n追加してください");
		}
	}
	
	// ScoreInputContainerのマウスイベント
	public void OnClickScoreInputPrevButton() {
		ResetShowAdsCounter();
		SaveInprogressData();
		StartCoroutine(ClickScoreInputPrevButton());
	}

	private IEnumerator ClickScoreInputPrevButton() {
		int count = 0;
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			count = node.RemoveScoreListNodeObject();
		}
		
		int lastIndex = TurnLabelListNodeList.Count-1;
		if (TurnLabelListNodeList.Count != 0) {
			GameObject obj = TurnLabelListNodeList[lastIndex];
			TurnLabelListNodeList.RemoveAt(lastIndex);
			Destroy(obj);
		}

		if (count == 0) {
			// 最初の入力状態だったら、プレイヤー名入力に戻る
			NowState = State.PlayerNameInputInit;
			InitTotalScore();
			InitRanking();
		} else {
			SetClickableTextListEnable(true);
			UpdateTotalScore();
			UpdateRanking();

			yield return null;// スクロールビューへの反映を待ちたいので1フレーム遅らせる

			OnScrollValueChange(Vector2.one);
		}
	}
	
	public void OnClickScoreInputNextButton() {
		ResetShowAdsCounter();
		if (TurnLabelListNodeList.Count < TurnCountMax) {
			SaveInprogressData();
			StartCoroutine(ClickScoreInputNextButton());
		} else {
			MessageDialogComponent.Open("ごめんなさい\nこれ以上ターン数は追加できません");
		}
	}

	private IEnumerator ClickScoreInputNextButton() {
		bool findEnpty = false;
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			if (string.IsNullOrEmpty(node.GetScoreText()) == true) {
				findEnpty = true;
				break;
			}
		}

		if (findEnpty == true) {
			MessageDialogComponent.Open("スコアが入力されていない\nプレイヤーが存在します");
			yield break;
		}

		SetClickableTextListEnable(false);
		UpdateTotalScore();
		UpdateRanking();
		AddScoreInputListNode();
		AddTurnLabelListNode();

		yield return null;// スクロールにアイテムを適用させるために、1フレーム遅らせる

		OnScrollValueChange(Vector2.one);
	}
	
	public void OnClickScoreInputResultButton() {
		ResetShowAdsCounter();
		SaveInprogressData();
		bool findEmptyNode = false;
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			string score = node.GetScoreText();
			if (string.IsNullOrEmpty(score) == true) {
				findEmptyNode = true;
				break;
			}
		}

		if (findEmptyNode == false) {
			SetClickableTextListEnable(false);
			UpdateTotalScore();
			UpdateRanking();
			NowState = State.ResultInit;
		} else {
			MessageDialogComponent.Open("スコアが入力されていない\nプレイヤーが存在します");
		}
	}
	
	public void OnClickScoreInputAddButton() {
		ResetShowAdsCounter();
		if (PlayerScoreListNodeList.Count < PlayerCountMax) {
			PlayerScoreListNode node = AddPlayerScoreListNode();
			for (int i = 0; i < TurnLabelListNodeList.Count; i++) {
				node.AddScoreListNode(true);
				node.SetupScoreListNode();
			}
			UpdateTotalScore();
			UpdateRanking();
			
			OnScrollValueChange(Vector2.one);
		
		} else {
			string str = string.Format("これ以上人数は増やせません。\n(最大{0}人まで)", PlayerCountMax);
			MessageDialogComponent.Open(str);
		}
	}
	
	// ResultContainerのマウスイベント
	public void OnClickResultBackButton() {
		ResetShowAdsCounter();

		NowState = State.ScoreInputInit;
	}
	
	public void OnClickResultSaveButton() {
		ResetShowAdsCounter();
		GoogleAdmobManager.Instance.ShowAdmob();
		SaveDialogComponent.Open(SaveGameData);
	}
	
	public void OnClickResultKeepRestartButton() {
		ResetShowAdsCounter();
		ClearInprogressData();
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.RemoveAllScoreListNodeObject();
		}
		for (int i = 0; i < TurnLabelListNodeList.Count; i++) {
			TurnLabelListNodeList[i].transform.parent = null;
			Destroy(TurnLabelListNodeList[i]);
		}
		TurnLabelListNodeList.Clear();

		NowState = State.PlayerNameInputInit;
	}
	
	public void OnClickResultClearRestartButton() {
		ResetShowAdsCounter();
		ClearInprogressData();
		Initialize();
	}
	
	private void AddTurnLabelListNode() {
		GameObject node = Instantiate(TurnLabelListNode);
		node.transform.SetParent(TopScrollView.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		TurnLabelListNodeList.Add(node);
		int count = TurnLabelListNodeList.Count;
		TurnListNode turnNode = node.GetComponent<TurnListNode>();
		turnNode.Setup();
		turnNode.SetTurnText("Turn" + count.ToString());
	}

	private PlayerScoreListNode AddPlayerScoreListNode() {
		GameObject node = Instantiate(PlayerScoreListNode);
		node.transform.SetParent(BaseScrollView.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		PlayerScoreListNodeList.Add(node);
		PlayerScoreListNode listNode = node.GetComponent<PlayerScoreListNode>();
		if (NowState == State.ScoreInputUpdate) {
			listNode.Setup(PlayerNameInputEndEditCallback, PlayerScoreListNodeList.Count-1, OnScrollValueChange, OpenScoreInputer, RemovePlayerListNode, true);
		} else {
			listNode.Setup(PlayerNameInputEndEditCallback, PlayerScoreListNodeList.Count-1, OnScrollValueChange, OpenScoreInputer, RemovePlayerListNode, false);
		}

		return listNode;
	}

	void PlayerNameInputEndEditCallback(List<string> inputStrings) {
	}
	
	void SetClickableTextListEnable(bool enable) {
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.SetClickableTextEnable(enable);
		}
	}

	void UpdateTotalScore() {
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.UpdateTotalScoreText();
		}
	}

	void InitTotalScore() {
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.InitTotalScoreText();
		}
	}

	bool IsScoreListCountZero() {
		bool isZero = false;
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			int count = node.GetScoreListNodeList().Count;
			if (count == 0) {
				isZero = true;
				break;
			}
		}

		return isZero;
	}

	void UpdateRanking() {
		//List<PlayerScoreListNode> sortList = new List<PlayerScoreListNode>(PlayerScoreListNodeList);
		List<PlayerScoreListNode> sortList = new List<PlayerScoreListNode>();

		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			sortList.Add(PlayerScoreListNodeList[i].GetComponent<PlayerScoreListNode>());
		}

		// ランキングソートするリストに、ノードの番号とスコアを入れておく
		// スコアでソートする
		// ノードの番号で、どのノードが何位なのかわかる。
		//sortList.Sort(SortByScoreDescendingOrder);

		if (NowRuleConfing == RuleConfigState.DescendingOrder) {
			sortList.Sort(SortByScoreDescendingOrder);
		} else {
			sortList.Sort(SortByScoreAscendingOrder);
		}

		int rank = 1;
		int prevScore = 0;
		int sameCount = 0;
		for (int i = 0; i < sortList.Count; i++) {
			PlayerScoreListNode node = sortList[i];
			int score = int.Parse(node.GetTotalScoreText());
			if (i == 0) {
				prevScore = score;
				node.SetRankText(rank.ToString());
			} else {
				if (score == prevScore) {
					node.SetRankText(rank.ToString());
					sameCount++;
				} else {
					rank = (rank + 1) + sameCount;
					node.SetRankText(rank.ToString());
					sameCount = 0;
					prevScore = score;
				}
			}
		}
	}

	// 降順(点数が大きい順)
	private int SortByScoreDescendingOrder(PlayerScoreListNode left, PlayerScoreListNode right) {
		return int.Parse(right.GetTotalScoreText()) - int.Parse(left.GetTotalScoreText());
	}
	
	// 昇順(点数が小さい順)
	private int SortByScoreAscendingOrder(PlayerScoreListNode left, PlayerScoreListNode right) {
		return int.Parse(left.GetTotalScoreText()) - int.Parse(right.GetTotalScoreText());
	}
	
	void InitRanking() {
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			PlayerScoreListNode node = PlayerScoreListNodeList[i].GetComponent<PlayerScoreListNode>();
			node.SetRankText("0");
		}
	}

	private void UpdateRuleConfigString() {
		HeaderContainer header = HeaderContainer.GetComponent<HeaderContainer>();

		if (NowRuleConfing == RuleConfigState.DescendingOrder) {
			header.SetRuleConfigText("合計が多い方が1位");
		} else if (NowRuleConfing == RuleConfigState.AscendingOrder) {
			header.SetRuleConfigText("合計が少ない方が1位");
		}
	}

	public void OnScrollValueChange(Vector2 pos) {
		TopScrollView.normalizedPosition = pos;
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			PlayerScoreListNode node = PlayerScoreListNodeList[i].GetComponent<PlayerScoreListNode>();
			node.UpdateScrollValue(pos);
		}

		ResetShowAdsCounter();
	}

	private void OpenScoreInputer(Text outputText) {
		ScoreInputer scoreInputer = ScoreInputer.GetComponent<ScoreInputer>();
		scoreInputer.Open(outputText, CloseScoreInputer);
	}

	private void CloseScoreInputer() {
		ResetShowAdsCounter();
	}

	// 後でStringUtiliryに移すけど、とりあえず文字列系操作の処理を記載していく
	private string SaveInprogressData() {
		string saveString = "";
		saveString += string.Format("{0},{1}\n", InprogressPlayerNumKey, PlayerScoreListNodeList.Count);
		saveString += string.Format("{0},{1}\n", InprogressTurnKey, TurnLabelListNodeList.Count);
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			string playerDataString = "";
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			playerDataString = string.Format("{0},{1}", InprogressNameKey, node.GetName());
			List<GameObject> scoreNodeList = node.GetScoreListNodeList();
			for (int j = 0; j < scoreNodeList.Count; j++) {
				ScoreListNode scoreNode = scoreNodeList[j].GetComponent<ScoreListNode>();
				string score = scoreNode.GetScoreText();
				playerDataString += "," + score;
			}
			saveString += playerDataString;
			string totalScore = node.GetTotalScoreText();
			saveString += "," + totalScore;
			string rank = node.GetRankingText();
			saveString += "," + rank;
			saveString += "\n";

#if UNITY_EDITOR
			Debug.Log(saveString);
#endif
		}

		PlayerPrefs.SetString(InprogressDataKey, saveString);
		
		return saveString;
	}
	private void ClearInprogressData() {
		PlayerPrefs.SetString(InprogressDataKey, "");
	}
	
	private void CheckInprogressData() {
		string s = PlayerPrefs.GetString(InprogressDataKey);
		if (string.IsNullOrEmpty(s) != true) {
			ParseInprogressData(s);
			if (TurnLabelListNodeList.Count == 0) {
				NowState = State.PlayerNameInputInit;
			} else {
				NowState = State.ScoreInputInit;
			}
//			UpdateTotalScore();
//			UpdateRanking();
		}
	}
	
	private void ParseInprogressData(string inprogressData) {
		char[] lineSplitRule = {'\n'};
		string[] lineStringList = inprogressData.Split(lineSplitRule);

		int turnNum = 0;
		for (int i = 0; i < lineStringList.Length; i++) {
			string lineString = lineStringList[i];
			char[] nameSplitRule = {','};
			string[] nameDataList = lineString.Split(nameSplitRule);

			if (nameDataList[0] == InprogressTurnKey) {
				turnNum = int.Parse(nameDataList[1]);
				for (int j = 0; j < turnNum; j++) {
					AddTurnLabelListNode();
				}
			}

			if (nameDataList[0] == InprogressNameKey) {

				PlayerScoreListNode node = AddPlayerScoreListNode();
				int index = 1;
				node.SetName(nameDataList[index]);
				index++;
				int scoreDataCount = nameDataList.Length-1-1-1-1;// データ個数-名前-キー-合計値とランク
				for (int j = 0; j < scoreDataCount; j++) {
					node.AddScoreListNode(false);
					//if (string.IsNullOrEmpty(nameDataList[index])) {
					//	node.AddScoreListNode(true);
					//} else {
					//	node.AddScoreListNode(false);
					//}
					node.SetupScoreListNode();
					node.SetScoreListScoreText(nameDataList[index]);
					index++;
				}
				node.SetTotalScoreText(nameDataList[index]);
				index++;
				node.SetRankText(nameDataList[index]);
			}
		}
	}

	void RemovePlayerListNode(PlayerScoreListNode deleteNode) {
		string playerName = deleteNode.GetName();
		string str = string.Format("{0}プレイヤーを\n削除しますか？", playerName);
		MessageDialogOkCancelComponent.Open(str, () => {
			for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
				GameObject obj = PlayerScoreListNodeList[i];
				PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
				if (node == deleteNode) {
					PlayerScoreListNodeList.Remove(obj);
					obj.transform.SetParent(null);
					Destroy(obj);
					break;
				}
			}
			UpdateTotalScore();
			UpdateRanking();
		});

	}
	
	void ResetShowAdsCounter() {
		ShowAdsCounter = 0;
		ShowAdsFlag = true;
	}
	
	void SetRemoveButtonActive(bool active) {
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.SetRemoveButtonActive(active);
		}
	}

	void SaveGameData(string title, string gameName) {
		DateTime now = DateTime.Now;
		
		string saveString = "";
		string saveInprogress = SaveInprogressData();
		string dateString = string.Format("{0},{1}\n", InprogressDateKey, now.ToString("yyyy/MM/dd/HH/mm/ss"));
		string titleString = string.Format("{0},{1}\n", InprogressTitleKey, title);
		string gameNameString = string.Format("{0},{1}\n", InprogressGameNameKey, gameName);
		saveString = string.Format("{0}{1}{2}", titleString, gameNameString, saveInprogress);


		string fileName = now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";
		CsvManager.Instance.SaveCsv(fileName, saveString);
	}
}
