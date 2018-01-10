using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class RewardAdButton : MonoBehaviour {

	public string AndroidId, IphoneId;

	private string rewardId;

	private Button button;
	private RewardBasedVideoAd rewardAd;

	private void Awake () {
		button = GetComponent<Button>();
	}

	private void Start () {
#if UNITY_ANDROID
		rewardId = AndroidId;
#elif UNITY_IOS
        rewardId = IphoneId;
#else
		rewardId = "unexpected_platform";
#endif

		// Get singleton reward based video ad reference.
		rewardAd = RewardBasedVideoAd.Instance;

		// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
		rewardAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
		rewardAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		rewardAd.OnAdOpening += HandleRewardBasedVideoOpened;
		rewardAd.OnAdStarted += HandleRewardBasedVideoStarted;
		rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
		rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;

		button.onClick.AddListener(RequestRewardBasedVideo);
	}

	private void RequestRewardBasedVideo () {
#if UNITY_ANDROID
		rewardId = AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/5224354917" : AndroidId;
#elif UNITY_IOS
        rewardId = AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/1712485313" : IphoneId;
#else
        rewardId = "unexpected_platform";
#endif

		rewardAd.LoadAd(AdMobiManager.Instance.adRequest, rewardId);
	}

	private void HandleRewardBasedVideoRewarded (object sender, Reward args) {
		string type = args.Type;
		double amount = args.Amount;
		print("Ganhou" + amount + " " + type);
	}

	private void HandleRewardBasedVideoLoaded (object sender, EventArgs args) {
		print("HandleRewardBasedVideoLoaded event received");
		if (rewardAd.IsLoaded()) rewardAd.Show();
	}

	private void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		print(
			"HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	private void HandleRewardBasedVideoOpened (object sender, EventArgs args) {
		print("HandleRewardBasedVideoOpened event received");
	}

	private void HandleRewardBasedVideoStarted (object sender, EventArgs args) {
		print("HandleRewardBasedVideoStarted event received");
	}

	private void HandleRewardBasedVideoClosed (object sender, EventArgs args) {
		print("HandleRewardBasedVideoClosed event received");
	}

}