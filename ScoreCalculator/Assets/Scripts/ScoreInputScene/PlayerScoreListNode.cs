using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreListNode : MonoBehaviour {
    [SerializeField] InputField NameInputField;
    [SerializeField] Text TotalText;
    [SerializeField] Text RankText;
    [SerializeField] ScrollRect ScoreScrollRext;

    public void Setup() {
    }

    public void SetName(string name) {
        NameInputField.text = name;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
