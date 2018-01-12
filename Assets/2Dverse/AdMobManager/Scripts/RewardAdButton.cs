using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardAdButton : MonoBehaviour {

	public string AndroidId, IphoneId;

	private string RewardId {
		get {
#if UNITY_EDITOR
			return "unexpected_platform";
#elif UNITY_IOS
 			return AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/1712485313" : IphoneId;
#elif UNITY_ANDROID
 			return AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/5224354917" : AndroidId;
#endif
		}
	}

	private Button button;

	private RewardBasedVideoAd rewardAd;

	private void Awake () {
		button = GetComponent<Button>();
	}

	private void Start () {
		// Get singleton reward based video ad reference.
		rewardAd = RewardBasedVideoAd.Instance;

		// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
		rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
		rewardAd.OnAdFailedToLoad += HandleFailedToLoad;
		rewardAd.OnAdClosed += HandleAdClosed;

		button.onClick.AddListener(ShowAd);
		RequestAd();
		StartCoroutine(CheckAdLoaded());
	}

	private void HandleAdClosed (object sender, EventArgs e) {
		RequestAd();
	}

	private void HandleFailedToLoad (object sender, AdFailedToLoadEventArgs e) {
		Debug.Log(e.Message);
		RequestAd();
	}

	private void HandleRewardBasedVideoRewarded (object sender, Reward args) {
		string type = args.Type;
		double amount = args.Amount;
		print("Ganhou " + amount + " " + type);
	}

	private void ShowAd () {
		rewardAd.Show();
	}

	private void RequestAd () {
		rewardAd.LoadAd(AdMobiManager.Instance.adRequest, RewardId);
	}

	private IEnumerator CheckAdLoaded () {
		while (true) {
			button.interactable = rewardAd.IsLoaded();
			yield return new WaitForEndOfFrame();
		}
	}

	private void OnDestroy () {
		// unregister handlers to prevent registering more than once
		rewardAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
		rewardAd.OnAdFailedToLoad -= HandleFailedToLoad;
		rewardAd.OnAdClosed -= HandleAdClosed;
	}

}