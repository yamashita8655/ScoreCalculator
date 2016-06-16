using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ============================================================
//!		@brief		ステートマシンマネージャ
//!		@note		シングルトン。ステートマシンの作成、実行、管理
//!		@attention	
//!		@warning	
// ============================================================
public class StateMachineManager
{
	private static StateMachineManager mInstance;
	
	private StateMachineManager () { // Private Constructor
		Debug.Log("Create SampleSingleton instance.");
	}
	
	public static StateMachineManager Instance {
		get {
			if( mInstance == null ) mInstance = new StateMachineManager();
			
			return mInstance;
		}
	}

	Dictionary<STATEMACHINE_NAME, StateMachine> mStateMachineMap = new Dictionary<STATEMACHINE_NAME, StateMachine>(); //< ステートマシンのマップ

	// ============================================================
	//!		@brief		初期化
	//!		@note		
	//!		@return		
	// ============================================================
	public void init() {
	}
	
	// ============================================================
	//!		@brief		ステートマシン作成
	//!		@note		
	//!		@return		
	// ============================================================
	public void createStateMachineMap() {
		mStateMachineMap.Add(STATEMACHINE_NAME.BATTLE,	new StateMachine());
		mStateMachineMap.Add(STATEMACHINE_NAME.NETWORK,	new StateMachine());
		mStateMachineMap.Add(STATEMACHINE_NAME.TEST,	new StateMachine());
	}
		
	// ============================================================
	//!		@brief		ステートマシン解放
	//!		@note		
	//!		@return		
	// ============================================================
	public void release(STATEMACHINE_NAME machineName) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);
		
		val.release();
	}
		
	public void addState(STATEMACHINE_NAME machineName, int stateType, StateBase state) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);
		
		val.addState(stateType, state);
	}

	public void changeState(STATEMACHINE_NAME machineName, int stateType) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);
		
		val.changeState(stateType);
	}

	public void changeStateNowStatePause(STATEMACHINE_NAME machineName, int stateType) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);

		val.changeStateNowStatePause(stateType);
	}

	public void update(STATEMACHINE_NAME machineName, float delta) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);
		
		val.update(delta);
	}

	public int getPrevState(STATEMACHINE_NAME machineName) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);

		return val.getPrevState();
	}

	public int getState(STATEMACHINE_NAME machineName) {
		StateMachine val = null;
		mStateMachineMap.TryGetValue(machineName, out val);

		return val.getState();
	}
}

