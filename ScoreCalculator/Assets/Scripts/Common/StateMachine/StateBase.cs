using UnityEngine;
using System.Collections;

// ============================================================
//!		@brief		ステートベースクラス
//!		@note		
//!		@attention	
//!		@warning	
// ============================================================
public class StateBase
{
	private bool mIsPause;// 今のステートが、次のChangeStateで有効になるまで動作させなくする。次の有効で、自動的にfalseにする
	private bool mInitCalled;// 

	public StateBase() {
		mIsPause = false;
	}

	public bool isPause() {
		return mIsPause;
	}

	public void pauseEnable() {
		mIsPause = true;
	}

	public void pauseDisable() {
		mIsPause = false;
	}

	virtual public void _onBeforeInit() {
		mInitCalled = true;
		onBeforeInit();
	}

	virtual public void _onRelease() {
		mInitCalled = false;
		onRelease();
	}

	public bool isInitCalled() {
		return mInitCalled;
	}

//	void beforeInit() {
//		
//	}
//	bool updateInit(float delta) {
//		return true;
//	}
//	void afterInit() {
//		
//	}
//
//	void beforeMain() {
//		
//	}
//	void updateMain(float delta) {
//	}
//	void afterMain() {
//		
//	}
//
//	void beforeEnd() {
//		
//	}
//	bool updateEnd(float delta) {
//		return true;
//	}
//	void afterEnd() {
//		
//	}
//	void release() {
//		
//	}
		
	virtual public void onBeforeInit() {
		//beforeInit();
		Debug.Log("StateBase : onBeforeInit");
	}
	virtual public bool onUpdateInit(float delta) {
		//return updateInit(delta);
		Debug.Log("StateBase : onUpdateInit");
		return true;
	}
	virtual public void onAfterInit() {
		Debug.Log("StateBase : onAfterInit");
		//afterInit();
	}

	virtual public void onBeforeMain() {
		Debug.Log("StateBase : onBeforeMain");
		//beforeMain();
	}
	virtual public void onUpdateMain(float delta) {
		Debug.Log("StateBase : onUpdateMain");
		//updateMain(delta);
	}
	virtual public void onAfterMain() {
		Debug.Log("StateBase : onAfterMain");
		//afterMain();
	}

	virtual public void onBeforeEnd() {
		Debug.Log("StateBase : onBeforeEnd");
		//beforeEnd();
	}
	virtual public bool onUpdateEnd(float delta) {
		Debug.Log("StateBase : onUpdateEnd");
		//return updateEnd(delta);
		return true;
	}
	virtual public void onAfterEnd() {
		Debug.Log("StateBase : onAfterEnd");
		//afterEnd();
	}

	virtual public void onRelease() {
		Debug.Log("StateBase : onRelease");
		//release();
	}
}

