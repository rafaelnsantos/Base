using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobiManager : MonoBehaviour {
    public string AndroidAppId;
    public string IphoneAppId;

    public string AndroidBannerId;
    public string IphoneBannerId;

    public string AndroidInterstitialId;
    public string IphoneInterstitialId;

    public string AndroidRewardedVideoId;
    public string IphoneRewardedVideoId;

    public static AdMobiManager Instance { get; private set; }

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private NativeExpressAdView nativeExpressAdView;
    private RewardBasedVideoAd rewardBasedVideo;

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
#if UNITY_EDITOR
        string appId = "unused";
#elif UNITY_ANDROID
        string appId = AndroidAppId;
#elif UNITY_IPHONE
        string appId = IphoneAppId;
#else
        string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest () {
        return new AdRequest.Builder()
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1991, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    public void RequestBanner () {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = AndroidBannerId;
#elif UNITY_IPHONE
        string adUnitId = IphoneBannerId;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (bannerView != null) {
            bannerView.OnAdLoaded -= HandleAdLoaded;
            bannerView.OnAdFailedToLoad -= HandleAdFailedToLoad;
            bannerView.OnAdOpening -= HandleAdOpened;
            bannerView.OnAdClosed -= HandleAdClosed;
            bannerView.OnAdLeavingApplication -= HandleAdLeftApplication;
            bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

        // Register for ad events.
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;

        // Load a banner ad.
        bannerView.LoadAd(CreateAdRequest());
    }

    public void RequestInterstitial () {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = AndroidInterstitialId;
#elif UNITY_IPHONE
        string adUnitId = IphoneInterstitialId;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial ad before creating a new one.
        if (interstitial != null) {
            interstitial.OnAdLoaded -= HandleInterstitialLoaded;
            interstitial.OnAdFailedToLoad -= HandleInterstitialFailedToLoad;
            interstitial.OnAdOpening -= HandleInterstitialOpened;
            interstitial.OnAdClosed -= HandleInterstitialClosed;
            interstitial.OnAdLeavingApplication -= HandleInterstitialLeftApplication;
            interstitial.Destroy();
        }

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        interstitial.LoadAd(CreateAdRequest());
    }

    public void RequestRewardBasedVideo () {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = AndroidRewardedVideoId;
#elif UNITY_IPHONE
        string adUnitId = IphoneRewardedVideoId;
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardBasedVideo.LoadAd(CreateAdRequest(), adUnitId);
    }

    private void ShowInterstitial () {
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        } else {
            print("Interstitial is not ready yet");
        }
    }

    private void ShowRewardBasedVideo () {
        if (rewardBasedVideo.IsLoaded()) {
            rewardBasedVideo.Show();
        } else {
            print("Reward based video ad is not ready yet");
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded (object sender, EventArgs args) {
        print("HandleAdLoaded event received");
        bannerView.Show();
    }

    public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened (object sender, EventArgs args) {
        print("HandleAdOpened event received");
    }

    public void HandleAdClosed (object sender, EventArgs args) {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication (object sender, EventArgs args) {
        print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded (object sender, EventArgs args) {
        print("HandleInterstitialLoaded event received");
        ShowInterstitial();
    }

    public void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened (object sender, EventArgs args) {
        print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed (object sender, EventArgs args) {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication (object sender, EventArgs args) {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region Native express ad callback handlers

    public void HandleNativeExpressAdLoaded (object sender, EventArgs args) {
        print("HandleNativeExpressAdAdLoaded event received");
    }

    public void HandleNativeExpresseAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        print(
            "HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleNativeExpressAdOpened (object sender, EventArgs args) {
        print("HandleNativeExpressAdAdOpened event received");
    }

    public void HandleNativeExpressAdClosed (object sender, EventArgs args) {
        print("HandleNativeExpressAdAdClosed event received");
    }

    public void HandleNativeExpressAdLeftApplication (object sender, EventArgs args) {
        print("HandleNativeExpressAdAdLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded (object sender, EventArgs args) {
        print("HandleRewardBasedVideoLoaded event received");
        ShowRewardBasedVideo();
    }

    public void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened (object sender, EventArgs args) {
        print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted (object sender, EventArgs args) {
        print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed (object sender, EventArgs args) {
        print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded (object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardBasedVideoRewarded event received for " + amount + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication (object sender, EventArgs args) {
        print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}