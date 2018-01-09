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
#elif UNITY_IPHONE
        rewardId = IphoneId;
#else
		rewardId = "unexpected_platform";
#endif

		button.onClick.AddListener(LoadAd);
	}

	private void LoadAd () {
		AdMobiManager.Instance.RequestRewardBasedVideo(rewardId);
		rewardAd = AdMobiManager.Instance.RewardBasedVideo;
		rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
		rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;
	}

	private void HandleRewardBasedVideoRewarded (object sender, Reward args) {
		string type = args.Type;
		double amount = args.Amount;
		print("Ganhou" + amount + " " + type);
	}

	private void HandleRewardBasedVideoClosed (object sender, EventArgs args) {
		rewardAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
		rewardAd.OnAdClosed -= HandleRewardBasedVideoClosed;
	}

}