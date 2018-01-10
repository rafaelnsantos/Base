using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdBanner : MonoBehaviour {

	public enum SizeAd {

		SmartBanner,
		Banner,
		IabBanner,
		MediumRectangle

	}

	public SizeAd BannerSize;
	public AdPosition BannerPosition;

	public string AndroidBannerId, IphoneBannerId;
	private BannerView bannerView;
	private string bannerId;
	private AdSize adSize;

	private void Start () {
		// These ad units are configured to serve test ads if teste is true.
#if UNITY_ANDROID
		bannerId = AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/6300978111" : AndroidBannerId;
#elif UNITY_IOS
        bannerId = AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/2934735716" : IphoneBannerId;
#else
		bannerId = "unexpected_platform;
#endif

		switch (BannerSize) {
			case SizeAd.Banner:
				adSize = AdSize.Banner;
				break;
			case SizeAd.IabBanner:
				adSize = AdSize.IABBanner;
				break;
			case SizeAd.MediumRectangle:
				adSize = AdSize.MediumRectangle;
				break;
			case SizeAd.SmartBanner:
				adSize = AdSize.SmartBanner;
				break;
			default:
				adSize = AdSize.SmartBanner;
				break;
		}

		SceneLoader.Instance.OnSceneLoad += DestroyBanner;
		RequestBanner();
	}

	private void RequestBanner () {
		DestroyBanner();
		bannerView = new BannerView(bannerId, adSize, BannerPosition);
		bannerView.OnAdLoaded += TempBannerLoaded;
		bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
		bannerView.OnAdClosed += HandleAdClosed;
		bannerView.LoadAd(AdMobiManager.Instance.adRequest);
	}

	private void DestroyBanner () {
		if (bannerView == null) return;

		bannerView.OnAdLoaded -= TempBannerLoaded;
		bannerView.OnAdFailedToLoad -= HandleAdFailedToLoad;
		bannerView.OnAdClosed -= HandleAdClosed;
		bannerView.Destroy();
	}

	#region Banner callback handlers

	private void TempBannerLoaded (object sender, EventArgs e) {
		print("HandleTempAdLoaded event received");
		bannerView.Show();
	}

	private void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	private void HandleAdClosed (object sender, EventArgs args) {
		print("HandleAdClosed event received");
	}

	#endregion

	private void OnDestroy () {
		SceneLoader.Instance.OnSceneLoad -= DestroyBanner;
	}

}