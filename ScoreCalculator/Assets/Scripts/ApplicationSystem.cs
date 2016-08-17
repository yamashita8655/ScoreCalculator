using UnityEngine;
using System.Collections;

public class ApplicationSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.Instance.Initialize();
		//UnityAdsManager.Instance.Initialize();
		CsvManager.Instance.Initialize();
	}
	
	// Update is called once per frame
	//void Update () {
	//
	//}
}
