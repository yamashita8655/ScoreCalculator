using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : Singleton<SceneManager> {

	[SerializeField] GameObject TitleSceneObject;
	[SerializeField] GameObject ScoreInputSceneObject;

	public static readonly string TitleScene = "TitleScene";
	public static readonly string ScoreInputScene = "ScoreInputScene";
	public static readonly Dictionary<string, GameObject> SceneObjectDict = new Dictionary<string, GameObject>();

	private GameObject CurrentScene = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Initialize() {
		Debug.Log("SceneMangaer init");
		SceneObjectDict.Add(TitleScene, TitleSceneObject);
		SceneObjectDict.Add(ScoreInputScene, ScoreInputSceneObject);
		TitleSceneObject.SetActive(true);

		SceneBase scene = TitleSceneObject.GetComponent<SceneBase>();
		scene.Initialize();

		CurrentScene = TitleSceneObject;
	}

	public void ChangeScene(string sceneName) {
		GameObject sceneObj = null;
		SceneObjectDict.TryGetValue(sceneName, out sceneObj);

		if (sceneObj != null) {
			CurrentScene.SetActive(false);
			sceneObj.SetActive(true);
			SceneBase scene = sceneObj.GetComponent<SceneBase>();
			scene.Initialize();
			CurrentScene = sceneObj;
		}
	}
}
