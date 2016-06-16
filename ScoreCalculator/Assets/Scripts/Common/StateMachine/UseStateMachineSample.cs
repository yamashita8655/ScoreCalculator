using UnityEngine;
using System.Collections;

public class UseStateMachineSample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// マネージャ自体の初期化
		StateMachineManager.Instance.init();
		StateMachineManager.Instance.createStateMachineMap();
		
		// マネージャにステートマシン登録
		StateMachineManager.Instance.addState(STATEMACHINE_NAME.TEST, 1, new StateMachine_test1());
		StateMachineManager.Instance.addState(STATEMACHINE_NAME.TEST, 2, new StateMachine_test2());
		
		// 初期ステート設定
		StateMachineManager.Instance.changeState(STATEMACHINE_NAME.TEST, 1);
	}
	
	// Update is called once per frame
	void Update () {
		float delta = Time.deltaTime;
		StateMachineManager.Instance.update(STATEMACHINE_NAME.TEST, delta);

		if (Input.GetKeyDown(KeyCode.Space)) {
			StateMachineManager.Instance.changeState(STATEMACHINE_NAME.TEST, 2);
		}
	}
}
