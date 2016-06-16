using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreInputScene : SceneBase {

	[SerializeField]	GameObject	HeaderContainer;
	[SerializeField]	GameObject	PlayerNameInputContainer;
	[SerializeField]	GameObject	ScoreInputContainer;
	[SerializeField]	GameObject	ResultContainer;

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
	}
	
	void PlayerNameInputUpdate() {
	}
	
	void ScoreInputInit() {
		ToggleContainer(ToggleType.ScoreInput);
		NowState = State.ScoreInputUpdate;
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
}
