using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdInterstitial : MonoBehaviour {

	public string AndroidId, IphoneId;

	private string interstitialId;

	private InterstitialAd interstitialAd;

	private void Start () {
#if UNITY_ANDROID
		interstitialId = AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/1033173712" : AndroidId;
#elif UNITY_IOS
        interstitialId = AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/4411468910" : IphoneId;
#else
        interstitialId = "unexpected_platform";
#endif

		SceneLoader.Instance.OnSceneLoad += RequestInterstitial;
		SceneLoader.Instance.OnLoadedScene += ShowAds;
	}

	private void RequestInterstitial () {
		// Clean up interstitial ad before creating a new one.
		 DestroyInterstitial();

		// Create an interstitial.
		interstitialAd = new InterstitialAd(interstitialId);

		// Register for ad events.
		interstitialAd.OnAdLoaded += HandleInterstitialLoaded;
		interstitialAd.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitialAd.OnAdClosed += HandleInterstitialClosed;

		// Load an interstitial ad.
		interstitialAd.LoadAd(AdMobiManager.Instance.adRequest);
	}

	private void DestroyInterstitial () {
		if (interstitialAd == null) return;
		interstitialAd.OnAdLoaded -= HandleInterstitialLoaded;
		interstitialAd.OnAdFailedToLoad -= HandleInterstitialFailedToLoad;
		interstitialAd.OnAdClosed -= HandleInterstitialClosed;
		interstitialAd.Destroy();
	}

	private void HandleInterstitialLoaded (object sender, EventArgs args) {
		print("HandleInterstitialLoaded event received");
	}

	private void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		print("InterstitialFailedToLoad with message: " + args.Message);
		DestroyInterstitial();
	}

	private void HandleInterstitialClosed (object sender, EventArgs args) {
		print("HandleInterstitialClosed event received");
		DestroyInterstitial();
	}

	private void ShowAds () {
		if (interstitialAd.IsLoaded()) interstitialAd.Show();
	}

	private void OnDestroy () {
		SceneLoader.Instance.OnSceneLoad -= RequestInterstitial;
	}

}