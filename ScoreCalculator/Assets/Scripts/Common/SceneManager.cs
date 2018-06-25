using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : Singleton<SceneManager> {

	[SerializeField] GameObject TitleSceneObject;
	[SerializeField] GameObject ScoreInputSceneObject;
	[SerializeField] GameObject ResultSelectSceneObject;

	public static readonly string TitleScene = "TitleScene";
	public static readonly string ScoreInputScene = "ScoreInputScene";
	public static readonly string ResultSelectScene = "ResultSelectScene";
	public static readonly Dictionary<string, GameObject> SceneObjectDict = new Dictionary<string, GameObject>();

	private GameObject CurrentScene = null;

	public void Initialize() {
		SceneObjectDict.Add(TitleScene, TitleSceneObject);
		SceneObjectDict.Add(ScoreInputScene, ScoreInputSceneObject);
		SceneObjectDict.Add(ResultSelectScene, ResultSelectSceneObject);
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

	public GameObject GetCurrentScene() {
		return CurrentScene;
	}
}
