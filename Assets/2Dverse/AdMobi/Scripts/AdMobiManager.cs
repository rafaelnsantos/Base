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

    public static AdMobiManager Instance;

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
#if UNITY_ANDROID
        string appId = AndroidAppId;
#elif UNITY_IPHONE
        string appId = IphoneAppId;
#else
        string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest () {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
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
        if (this.bannerView != null) {
            this.bannerView.OnAdLoaded -= this.HandleAdLoaded;
            this.bannerView.OnAdFailedToLoad -= this.HandleAdFailedToLoad;
            this.bannerView.OnAdOpening -= this.HandleAdOpened;
            this.bannerView.OnAdClosed -= this.HandleAdClosed;
            this.bannerView.OnAdLeavingApplication -= this.HandleAdLeftApplication;
            this.bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
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
        if (this.interstitial != null) {
            this.interstitial.OnAdLoaded -= this.HandleInterstitialLoaded;
            this.interstitial.OnAdFailedToLoad -= this.HandleInterstitialFailedToLoad;
            this.interstitial.OnAdOpening -= this.HandleInterstitialOpened;
            this.interstitial.OnAdClosed -= this.HandleInterstitialClosed;
            this.interstitial.OnAdLeavingApplication -= this.HandleInterstitialLeftApplication;
            this.interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    private void RequestNativeExpressAdView () {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1072772517";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2562852117";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up native express ad before creating a new one.
        if (this.nativeExpressAdView != null) {
            this.nativeExpressAdView.Destroy();
        }

        // Create a 320x150 native express ad at the top of the screen.
        this.nativeExpressAdView = new NativeExpressAdView(
            adUnitId,
            new AdSize(320, 150),
            AdPosition.Top);

        // Register for ad events.
        this.nativeExpressAdView.OnAdLoaded += this.HandleNativeExpressAdLoaded;
        this.nativeExpressAdView.OnAdFailedToLoad += this.HandleNativeExpresseAdFailedToLoad;
        this.nativeExpressAdView.OnAdOpening += this.HandleNativeExpressAdOpened;
        this.nativeExpressAdView.OnAdClosed += this.HandleNativeExpressAdClosed;
        this.nativeExpressAdView.OnAdLeavingApplication += this.HandleNativeExpressAdLeftApplication;

        // Load a native express ad.
        this.nativeExpressAdView.LoadAd(this.CreateAdRequest());
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

        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
    }

    private void ShowInterstitial () {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        } else {
            MonoBehaviour.print("Interstitial is not ready yet");
        }
    }

    private void ShowRewardBasedVideo () {
        if (this.rewardBasedVideo.IsLoaded()) {
            this.rewardBasedVideo.Show();
        } else {
            MonoBehaviour.print("Reward based video ad is not ready yet");
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded (object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdLoaded event received");
        this.bannerView.Show();
    }

    public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened (object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed (object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication (object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded (object sender, EventArgs args) {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
        ShowInterstitial();
    }

    public void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened (object sender, EventArgs args) {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed (object sender, EventArgs args) {
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication (object sender, EventArgs args) {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region Native express ad callback handlers

    public void HandleNativeExpressAdLoaded (object sender, EventArgs args) {
        MonoBehaviour.print("HandleNativeExpressAdAdLoaded event received");
    }

    public void HandleNativeExpresseAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        MonoBehaviour.print(
            "HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleNativeExpressAdOpened (object sender, EventArgs args) {
        MonoBehaviour.print("HandleNativeExpressAdAdOpened event received");
    }

    public void HandleNativeExpressAdClosed (object sender, EventArgs args) {
        MonoBehaviour.print("HandleNativeExpressAdAdClosed event received");
    }

    public void HandleNativeExpressAdLeftApplication (object sender, EventArgs args) {
        MonoBehaviour.print("HandleNativeExpressAdAdLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded (object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
        ShowRewardBasedVideo();
    }

    public void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened (object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted (object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed (object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded (object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication (object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}