using UnityEngine;
using System.Collections;

public class StateMachine_test1 : StateBase {
	
	// ============================================================
	//!		@brief		初期化
	//!		@note		外部公開
	//!		@return		
	// ============================================================
	override public void onAfterInit()
	{
		Debug.Log("StateMachine_test1 : afterInit");
	}

	// ============================================================
	//!		@brief		更新
	//!		@note		外部公開
	//!		@param[in]	delta 経過時間
	//!		@return		
	// ============================================================
	override public void onUpdateMain(float delta)
	{
		Debug.Log("StateMachine_test1 : updateMain");
	}

	// ============================================================
	//!		@brief		終了
	//!		@note		外部公開
	//!		@return		
	// ============================================================
	override public void onRelease()
	{
		Debug.Log("StateMachine_test1 : release");
	}
}
