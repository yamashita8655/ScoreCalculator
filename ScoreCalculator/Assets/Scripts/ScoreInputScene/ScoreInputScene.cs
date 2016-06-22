using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScoreInputScene : SceneBase {

	[SerializeField]	GameObject	HeaderContainer;
	[SerializeField]	GameObject	PlayerNameInputContainer;
	[SerializeField]	GameObject	ScoreInputContainer;
	[SerializeField]	GameObject	ResultContainer;
	[SerializeField]	GameObject	PlayerScoreListNode;
	[SerializeField]	ScrollRect	BaseScrollView;
	
	List<GameObject>	PlayerScoreListNodeList = new List<GameObject>();
	PlayerScoreListNode	NowSelectPlayerScoreListNode = null;
	int					NowSelectIndex = 0;

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

	State NowState = State.PlayerNameInputInit;
	
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
		}
		PlayerScoreListNodeList.RemoveAt(PlayerScoreListNodeList.Count-1);
		
		// スコア入力ノードを、プレイヤー数分一度に追加する
		for (int i = 0; i < PlayerScoreListNodeList.Count; i++) {
			GameObject obj = PlayerScoreListNodeList[i];
			PlayerScoreListNode node = obj.GetComponent<PlayerScoreListNode>();
			node.AddScoreListNode();
			node.SetupScoreListNode(ScoreInputEndEditCallback);
		}

		NowSelectIndex = 0;
		
		// 最初のノードだけ、入力可能にしておく
		GameObject firstObj = PlayerScoreListNodeList[NowSelectIndex];
		PlayerScoreListNode firstNode = firstObj.GetComponent<PlayerScoreListNode>();
		firstNode.SetEnableScoreInputField(true);
		firstNode.ActivateScoreInputField();
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
		// 最初の入力状態だったら、プレイヤー名入力に戻る
		//NowState = State.PlayerNameInputInit;
	}
	
	public void OnClickScoreInputNextButton() {
	}
	
	public void OnClickScoreInputResultButton() {
		NowState = State.ResultInit;
	}
	
	// ResultContainerのマウスイベント
	public void OnClickResultBackButton() {
		NowState = State.ScoreInputInit;
	}
	
	public void OnClickResultSaveButton() {
	}
	
	public void OnClickResultKeepRestartButton() {
	}
	
	public void OnClickResultClearRestartButton() {
	}


	private void AddPlayerScoreListNode() {
		GameObject node = Instantiate(PlayerScoreListNode);
		node.transform.SetParent(BaseScrollView.content);
		node.transform.position = Vector3.zero;
		node.transform.localScale = Vector3.one;

		PlayerScoreListNodeList.Add(node);
		NowSelectPlayerScoreListNode = node.GetComponent<PlayerScoreListNode>();
		NowSelectPlayerScoreListNode.Setup(PlayerNameInputEndEditCallback);
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

	void ScoreInputEndEditCallback(List<string> inputStrings) {
		GameObject nowObj = PlayerScoreListNodeList[NowSelectIndex];
		PlayerScoreListNode nodeNode = nowObj.GetComponent<PlayerScoreListNode>();
//		nodeNode.
//		NowSelectIndex = 0;
	}
}
