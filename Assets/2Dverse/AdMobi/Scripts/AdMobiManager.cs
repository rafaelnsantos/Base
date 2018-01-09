using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobiManager : MonoBehaviour {

	public string AndroidAppId;
	public string IphoneAppId;
	public bool Teste;

	public static AdMobiManager Instance { get; private set; }

	private BannerView banner;
	private InterstitialAd interstitial;
	public RewardBasedVideoAd RewardBasedVideo { get; private set; }
	private BannerView tempBanner;

	private void Awake () {
		// Singleton Pattern
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	private void Start () {
#if UNITY_ANDROID
		string appId = Teste ? "ca-app-pub-3940256099942544~3347511713" : AndroidAppId;
#elif UNITY_IPHONE
        string appId = Teste ? "ca-app-pub-3940256099942544~1458002511" : IphoneAppId;
#else
        string appId = "unexpected_platform";
#endif

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

		// Get singleton reward based video ad reference.
		RewardBasedVideo = RewardBasedVideoAd.Instance;

		// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
		RewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		RewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		RewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		RewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
//		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		RewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest () {
//		return new AdRequest.Builder()
//			.AddKeyword("game")
//			.SetGender(Gender.Male)
//			.SetBirthday(new DateTime(2005, 1, 1))
//			.TagForChildDirectedTreatment(true)
//			.AddExtra("color_bg", "9B30FF")
//			.Build();
		return Teste
			? new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).Build()
			: new AdRequest.Builder().Build();
	}

	public BannerView RequestBanner (string bannerId, AdSize size, AdPosition position) {
		// These ad units are configured to always serve test ads.
#if UNITY_ANDROID
		bannerId = Teste ? "ca-app-pub-3940256099942544/6300978111" : bannerId;
#elif UNITY_IPHONE
        bannerId = Teste ? "ca-app-pub-3940256099942544/2934735716" : bannerId;
#else
		bannerId = "unexpected_platform;
#endif
		// Clean up banner ad before creating a new one.
		if (banner != null) {
			tempBanner = new BannerView(bannerId, size, position);
			tempBanner.OnAdLoaded += TempBannerLoaded;
			tempBanner.OnAdFailedToLoad += HandleAdFailedToLoad;
			tempBanner.OnAdClosed += HandleAdClosed;
			tempBanner.LoadAd(CreateAdRequest());
			return tempBanner;
		}

		// Create a 320x50 banner at the top of the screen.
		banner = new BannerView(bannerId, size, position);

		// Register for ad events.
		banner.OnAdLoaded += HandleAdLoaded;
		banner.OnAdFailedToLoad += HandleAdFailedToLoad;
		banner.OnAdClosed += HandleAdClosed;

		// Load a banner ad.
		banner.LoadAd(CreateAdRequest());
		return banner;
	}



	public void RequestInterstitial (string adUnitId) {
		// These ad units are configured to always serve test ads.
#if UNITY_ANDROID
		adUnitId = Teste ? "ca-app-pub-3940256099942544/1033173712" : adUnitId;
#elif UNITY_IPHONE
        adUnitId = Teste ? "ca-app-pub-3940256099942544/4411468910" : adUnitId;
#else
        adUnitId = "unexpected_platform";
#endif

		// Clean up interstitial ad before creating a new one.
		if (interstitial != null) DestroyInterstitial();

		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);

		// Register for ad events.
		interstitial.OnAdLoaded += HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.OnAdClosed += HandleInterstitialClosed;

		// Load an interstitial ad.
		interstitial.LoadAd(CreateAdRequest());
	}

	public void RequestRewardBasedVideo (string adUnitId) {
#if UNITY_ANDROID
		adUnitId = Teste ? "ca-app-pub-3940256099942544/5224354917" : adUnitId;
#elif UNITY_IPHONE
        adUnitId = Teste ? "ca-app-pub-3940256099942544/1712485313" : adUnitId;
#else
        adUnitId = "unexpected_platform";
#endif

		RewardBasedVideo.LoadAd(CreateAdRequest(), adUnitId);
	}

	#region Banner callback handlers

	private void DestroyBanner () {
		banner.OnAdLoaded -= HandleAdLoaded;
		banner.OnAdFailedToLoad -= HandleAdFailedToLoad;
		banner.OnAdClosed -= HandleAdClosed;
		banner.Destroy();
	}
	
	private void TempBannerLoaded (object sender, EventArgs e) {
		print("HandleTempAdLoaded event received");
		DestroyBanner();
		banner = tempBanner;
		banner.Show();
	}

	private void HandleAdLoaded (object sender, EventArgs args) {
		print("HandleAdLoaded event received");
		banner.Show();
	}

	private void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	private void HandleAdClosed (object sender, EventArgs args) {
		print("HandleAdClosed event received");
		DestroyBanner();
	}

	#endregion

	#region Interstitial callback handlers

	private void DestroyInterstitial () {
		interstitial.OnAdLoaded -= HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad -= HandleInterstitialFailedToLoad;
		interstitial.OnAdClosed -= HandleInterstitialClosed;
		interstitial.Destroy();
	}

	private void HandleInterstitialLoaded (object sender, EventArgs args) {
		print("HandleInterstitialLoaded event received");
		if (interstitial.IsLoaded()) interstitial.Show();
	}

	private void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		print("InterstitialFailedToLoad with message: " + args.Message);
		DestroyInterstitial();
	}

	private void HandleInterstitialClosed (object sender, EventArgs args) {
		print("HandleInterstitialClosed event received");
		DestroyInterstitial();
	}

	#endregion

	#region RewardBasedVideo callback handlers

	private void HandleRewardBasedVideoLoaded (object sender, EventArgs args) {
		print("HandleRewardBasedVideoLoaded event received");
		if (RewardBasedVideo.IsLoaded()) RewardBasedVideo.Show();
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

//	private void HandleRewardBasedVideoRewarded (object sender, Reward args) {
//		string type = args.Type;
//		double amount = args.Amount;
//		print(
//			"HandleRewardBasedVideoRewarded event received for " + amount + " " + type);
//	}

	#endregion

}