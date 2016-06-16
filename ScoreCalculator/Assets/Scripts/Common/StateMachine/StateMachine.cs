using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum MACHINE_STATE : int
{
	NONE,
	BEFORE_INIT,
	UPDATE_INIT,
	AFTER_INIT,
	BEFORE_MAIN,
	UPDATE_MAIN,
	AFTER_MAIN,
	BEFORE_END,
	UPDATE_END,
	AFTER_END,
	RELEASE	
};

// ============================================================
//!		@brief		ステートマネージャ
//!		@note		シングルトン。ステートの切り替え、管理をする
//!		@attention	
//!		@warning	
// ============================================================
public class StateMachine {
	
	private StateBase mNowState;
	private MACHINE_STATE mManageState;
	private int mState;
	private int mNextState;
	private int mPrevState;

	private Dictionary<int, StateBase> mStateMap;

	public StateMachine() {
		init();
	}

	public void init(){
		mNowState       = null;
		mManageState	= MACHINE_STATE.NONE;
		mState			= 0;
		mNextState 		= 0;
		mPrevState 		= 0;
	
		mStateMap		= new Dictionary<int, StateBase>();
	}
	
	public void update(float delta){
		if(mNowState == null)
		{
			return;
		}

		switch(mManageState)
		{
		case MACHINE_STATE.BEFORE_INIT:
			if (mNowState.isPause() == true)
			{
				// ポーズ状態だったら、即メインのアップデート処理状態にする
				mManageState = MACHINE_STATE.UPDATE_MAIN;
				mNowState.pauseDisable();
			}
			else
			{
				mManageState = MACHINE_STATE.UPDATE_INIT;
				mNowState._onBeforeInit();
			}
			break;
		
		case MACHINE_STATE.UPDATE_INIT:
			{
				bool isEnd = mNowState.onUpdateInit(delta);
				if(isEnd == true)
				{
					mManageState = MACHINE_STATE.AFTER_INIT;
				}
			}
			break;
		
		case MACHINE_STATE.AFTER_INIT:
			mManageState = MACHINE_STATE.BEFORE_MAIN;
			mNowState.onAfterInit();
			break;
		
		case MACHINE_STATE.BEFORE_MAIN:
			mManageState = MACHINE_STATE.UPDATE_MAIN;
			mNowState.onBeforeMain();
			break;
		
		case MACHINE_STATE.UPDATE_MAIN:
			mNowState.onUpdateMain(delta);
			break;
		
		case MACHINE_STATE.AFTER_MAIN:
			mManageState = MACHINE_STATE.BEFORE_END;
			mNowState.onAfterMain();
			break;
		
		case MACHINE_STATE.BEFORE_END:
			mManageState = MACHINE_STATE.UPDATE_END;
			mNowState.onBeforeEnd();
			break;
		
		case MACHINE_STATE.UPDATE_END:
			{
				bool isEnd = mNowState.onUpdateEnd(delta);
				if(isEnd == true)
				{
					mManageState = MACHINE_STATE.AFTER_END;
				}
			}
			break;
		
		case MACHINE_STATE.AFTER_END:
			mManageState = MACHINE_STATE.RELEASE;
			mNowState.onAfterEnd();
			break;
		
		case MACHINE_STATE.RELEASE:
			{
				mManageState = MACHINE_STATE.BEFORE_INIT;
				if (mNowState.isPause() != true)
				{
					mNowState._onRelease();
				}
				mPrevState = mState;
				mState = mNextState;
				
				StateBase val = null;
				mStateMap.TryGetValue(mState, out val);
				mNowState = val;
				break;
			}
		}
	}
	
	public void release(){

		foreach(var it in mStateMap)
		{
			if(it.Value.isInitCalled() == true)
			{
				it.Value._onRelease();
			}
			//it.Value = null;
		}

		mStateMap.Clear();
		mNowState = null;
	}
	
	public void changeState(int stateType){
		// 仮 ここ、直す必要がある。途中で呼び出された場合に、流れがおかしくなる可能性がある
		if (mManageState != MACHINE_STATE.AFTER_INIT &&
			mManageState != MACHINE_STATE.UPDATE_MAIN		)
		{
			Debug.Log("StateMachine::changeState");
			Debug.Log((int)mManageState + " state isnt use changeState");
		}

		if(mNowState != null)
		{
			mNextState = stateType;
			mManageState = MACHINE_STATE.AFTER_MAIN;
		}
		else
		{
			StateBase val = null;
			mStateMap.TryGetValue(stateType, out val);
			mNowState = val;
			mManageState = MACHINE_STATE.BEFORE_INIT;
		}
	}
	
	public void changeStateNowStatePause(int stateType){
		if (mManageState != MACHINE_STATE.AFTER_INIT &&
			mManageState != MACHINE_STATE.UPDATE_MAIN)
		{
			Debug.Log("StateMachine::changeState");
			Debug.Log(mManageState + " state isnt use changeState");
		}

		if(mNowState != null)
		{
			mNowState.pauseEnable();
			mNextState = stateType;
			mManageState = MACHINE_STATE.RELEASE;// 即リリース状態にしているが、PAUSEをかけているので、リリースの処理は呼び出されない
		}
		else
		{
			StateBase val = null;
			mStateMap.TryGetValue(stateType, out val);
			mNowState = val;
			mManageState = MACHINE_STATE.BEFORE_INIT;
		}
	}

	public void addState(int stateType, StateBase state){
		StateBase val = null;
		if(mStateMap.TryGetValue(stateType, out val) == true) {
			return;
		}

		//mStateMap.insert(std::make_pair(stateType, state));
		mStateMap.Add(stateType, state);
	}
	
	public int getState(){
		return mState;
	}
	
	public int getPrevState(){
		return mPrevState;
	}
}
