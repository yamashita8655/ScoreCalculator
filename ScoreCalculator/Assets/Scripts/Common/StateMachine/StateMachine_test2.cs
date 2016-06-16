using UnityEngine;
using System.Collections;

public class StateMachine_test2 : StateBase {
	
	override public void onBeforeInit() {
		//beforeInit();
		Debug.Log("StateMachine_test2 : onBeforeInit");
	}
	override public bool onUpdateInit(float delta) {
		//return updateInit(delta);
		Debug.Log("StateMachine_test2 : onUpdateInit");
		return true;
	}
	// ============================================================
	//!		@brief		初期化
	//!		@note		外部公開
	//!		@return		
	// ============================================================
	override public void onAfterInit()
	{
		Debug.Log("StateMachine_test2 : afterInit");
	}

	override public void onBeforeMain() {
		Debug.Log("StateMachine_test2 : onBeforeMain");
	}
	// ============================================================
	//!		@brief		更新
	//!		@note		外部公開
	//!		@param[in]	delta 経過時間
	//!		@return		
	// ============================================================
	override public void onUpdateMain(float delta)
	{
		Debug.Log("StateMachine_test2 : updateMain");
	}
	override public void onAfterMain() {
		Debug.Log("StateMachine_test2 : onAfterMain");
	}
	
	override public void onBeforeEnd() {
		Debug.Log("StateMachine_test2 : onBeforeEnd");
	}
	override public bool onUpdateEnd(float delta) {
		Debug.Log("StateMachine_test2 : onUpdateEnd");
		return true;
	}
	override public void onAfterEnd() {
		Debug.Log("StateMachine_test2 : onAfterEnd");
	}

	// ============================================================
	//!		@brief		終了
	//!		@note		外部公開
	//!		@return		
	// ============================================================
	override public void onRelease()
	{
		Debug.Log("StateMachine_test2 : release");
	}
}
