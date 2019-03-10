using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;

public class GoogleAdmobManager : Singleton<GoogleAdmobManager> {

	private InterstitialAd InterstitialAd = null;
#if false
	// 広告ユニット ID を記述します
	private string adUnitId = "ca-app-pub-3940256099942544/1033173712";// テストらしい
#else
	private string adUnitId = "ca-app-pub-4566588771611947/9089539691";// こっちは、本番
#endif

    private bool isReady = false;

	public void Initialize() {
		InterstitialAd = new InterstitialAd(adUnitId);
		InterstitialAd.OnAdFailedToLoad += OnAdFailedToLoad;
		InterstitialAd.OnAdLoaded += OnAdLoaded;
		InterstitialAd.OnAdClosed += OnAdClosed;
        isReady = false;
        AdRequest request = new AdRequest.Builder().Build();
        InterstitialAd.LoadAd(request);
    }

    public void OnAdLoaded(object sender, System.EventArgs arg) {
        isReady = true;
    }

    public void OnAdClosed(object sender, System.EventArgs arg) {
	}

	public void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs arg) {
	}

	public bool IsInitialized() {
		return InterstitialAd.IsLoaded();
	}

	// 流れとしては
	// LoadAd->ちょっとかかる->OnAdLoaded->Show()で表示
	// Show()を行うと、自動的にLoadAdが呼び出されて準備が整うっぽい
	public void ShowAdmob() {
#if UNITY_EDITOR
#else
        if (isReady == true) {
            InterstitialAd.Show();
        }
#endif
    }
}
