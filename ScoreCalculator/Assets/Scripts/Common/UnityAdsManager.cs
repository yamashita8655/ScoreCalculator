using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsManager : Singleton<UnityAdsManager> {
	public void Initialize() {
		Debug.Log("UnityAdsManager init");
#if UNITY_EDITOR
//		Advertisement.Initialize("", true);
#else
//		Advertisement.Initialize("");
#endif
	}

	public void ShowAd() {
/*		if (Advertisement.IsReady()) {
			Advertisement.Show();
		}*/
	}
}
