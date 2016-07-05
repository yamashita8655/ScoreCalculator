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

	List<GameObject>	PlayerScoreListNodeList = new List<GameObject>();
	PlayerScoreListNode	NowSelectPlayerScoreListNode = null;

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
	
	//[SerializeField]	Button	StartCulcButton; 
	//[SerializeField]	Button	DataCheckButton; 
	
	//// Use this for initialization
	//void Start () {
	//
	//}
	//
	//// Update is called once per frame
	//void Update () {
	//
	//}
	
	override public void Initialize() {
		Debug.Log("ScoreInputScene");
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
		if (PlayerScoreListNodeList.Count == 0) {
			AddPlayerScoreListNode();
		}

		NowSelectPlayerScoreListNode = GetNowPlayerScoreListNode();
		NowSelectPlayerScoreListNode.SetEnableInputField(true);

		InitTotalScore();
		InitRanking();
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

		// 最後のリストが空の場合があるので、確認する
		GameObject lastObj = PlayerScoreListNodeList[PlayerScoreListNodeList.Count-1];
		PlayerScoreListNode lastNode = lastObj.GetComponent<PlayerScoreListNode>();
		if (string.IsNullOrEmpty(lastNode.GetName())) {
			Destroy(lastObj);
			PlayerScoreListNodeList.RemoveAt(PlayerScoreListNodeList.Count-1);
		}

		if (IsScoreListCountZero() == true) {
			AddScoreInputListNode();
			AddTurnLabelListNode();
		} else {
			SetClickableTextListEnable(true);
		}
	}

	private void AddScoreInputListNode() {
		// スコア入力ノードを、プレイヤー数分一度に追加する
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.AddScoreListNode();
			node.SetupScoreListNode();
		}
	}
	
	void ScoreInputUpdate() {
	}
	
	void ResultInit() {
		ToggleContainer(ToggleType.Result);
		NowState = State.ResultUpdate;
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
		SceneManager.Instance.ChangeScene(SceneManager.TitleScene);
	}
	
	public void OnClickHeaderRuleConfigButton() {
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
	}

	// PlayerNameInputContainerのマウスイベント
	public void OnClickPlayerNameInputPrevButton() {
		if (PlayerScoreListNodeList.Count == 1) {
			return;
		}
		
		GameObject nowSelectObj = PlayerScoreListNodeList[PlayerScoreListNodeList.Count-1];
		PlayerScoreListNodeList.RemoveAt(PlayerScoreListNodeList.Count-1);
		if (NowSelectPlayerScoreListNode != null) {
			NowSelectPlayerScoreListNode.transform.parent = null;
			Destroy(nowSelectObj);
			NowSelectPlayerScoreListNode = null;
	 	}

		NowSelectPlayerScoreListNode = PlayerScoreListNodeList[PlayerScoreListNodeList.Count-1].GetComponent<PlayerScoreListNode>();
		NowSelectPlayerScoreListNode.SetEnableInputField(true);
	}
	
	public void OnClickPlayerNameInputNextButton() {
	}
	
	public void OnClickPlayerNameInputInputEndButton() {
		NowState = State.ScoreInputInit;
	}
	
	// ScoreInputContainerのマウスイベント
	public void OnClickScoreInputPrevButton() {
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
		StartCoroutine(ClickScoreInputNextButton());
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
		}
	}
	
	// ResultContainerのマウスイベント
	public void OnClickResultBackButton() {
		NowState = State.ScoreInputInit;
	}
	
	public void OnClickResultSaveButton() {
	}
	
	public void OnClickResultKeepRestartButton() {
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

	private void AddPlayerScoreListNode() {
		GameObject node = Instantiate(PlayerScoreListNode);
		node.transform.SetParent(BaseScrollView.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		PlayerScoreListNodeList.Add(node);
		NowSelectPlayerScoreListNode = node.GetComponent<PlayerScoreListNode>();
		NowSelectPlayerScoreListNode.Setup(PlayerNameInputEndEditCallback, PlayerScoreListNodeList.Count-1, OnScrollValueChange, OpenScoreInputer);
	}

	void PlayerNameInputEndEditCallback(List<string> inputStrings) {
		NowSelectPlayerScoreListNode.SetEnableInputField(false);
		AddPlayerScoreListNode();

		for (int i = 1; i < inputStrings.Count; i++) {
			NowSelectPlayerScoreListNode.SetName(inputStrings[i]);
			NowSelectPlayerScoreListNode.SetEnableInputField(false);
			AddPlayerScoreListNode();
		}

		NowSelectPlayerScoreListNode.ActivateInputField();
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
			header.SetRuleConfigText("多い方が1位");
		} else if (NowRuleConfing == RuleConfigState.AscendingOrder) {
			header.SetRuleConfigText("少ない方が1位");
		}
	}

	public void OnScrollValueChange(Vector2 pos) {
		Debug.Log("ValueChange");
		Debug.Log(pos);

		TopScrollView.normalizedPosition = pos;
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			PlayerScoreListNode node = PlayerScoreListNodeList[i].GetComponent<PlayerScoreListNode>();
			node.UpdateScrollValue(pos);
		}
	}

	private void OpenScoreInputer(Text outputText) {
		ScoreInputer scoreInputer = ScoreInputer.GetComponent<ScoreInputer>();
		scoreInputer.Open(outputText);
	}
}
