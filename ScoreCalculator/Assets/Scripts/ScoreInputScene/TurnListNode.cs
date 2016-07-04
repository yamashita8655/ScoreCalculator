using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class TurnListNode : MonoBehaviour {
    [SerializeField] Text TurnText;

	public void Setup() {
    }
	
	public void SetTurnText(string text) {
		TurnText.text = text;
    }
}

