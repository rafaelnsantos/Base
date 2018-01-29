using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdBanner : MonoBehaviour {

	public enum Size {

		SmartBanner,
		Banner,
		IabBanner,
		MediumRectangle

	}

	public Size BannerSize;
	public AdPosition BannerPosition;

	public string AndroidBannerId, IphoneBannerId;
	private BannerView bannerView;

	private string BannerId {
		get {
#if UNITY_EDITOR || UNITY_WEBGL
			return "unexpected_platform";
#elif UNITY_IOS
			return AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/2934735716" : IphoneBannerId;
#elif UNITY_ANDROID
			return AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/6300978111" : AndroidBannerId;
#endif
		}
	}

	private AdSize SizeAd {
		get {
			switch (BannerSize) {
				case Size.Banner:
					return AdSize.Banner;
				case Size.IabBanner:
					return AdSize.IABBanner;
				case Size.MediumRectangle:
					return AdSize.MediumRectangle;
				case Size.SmartBanner:
					return AdSize.SmartBanner;
				default:
					return AdSize.SmartBanner;
			}
		}
	}

	private void Start () {

		if (AdMobiManager.Instance.NoAds) return;
		
		SceneLoader.Instance.OnSceneLoad += DestroyBanner;
		SceneLoader.Instance.OnLoadedScene += DestroyBanner;

		// Clean up banner ad before creating a new one.
		DestroyBanner();
		
		// Create a banner.
		bannerView = new BannerView(BannerId, SizeAd, BannerPosition);
		
		// Register handlers
		bannerView.OnAdLoaded += HandleBannerLoaded;
		
		// Load banner ad.
		bannerView.LoadAd(AdMobiManager.Instance.AdRequest);
	}

	private void DestroyBanner () {
		if (bannerView == null) return;
		bannerView.OnAdLoaded -= HandleBannerLoaded;
		bannerView.Destroy();
	}

	private void HandleBannerLoaded (object sender, EventArgs e) {
		bannerView.Show();
	}

	private void OnDestroy () {
		if (AdMobiManager.Instance.NoAds) return;
		SceneLoader.Instance.OnSceneLoad -= DestroyBanner;
		SceneLoader.Instance.OnLoadedScene -= DestroyBanner;
	}

}