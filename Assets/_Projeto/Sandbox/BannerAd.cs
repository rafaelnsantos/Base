using System;
using System.Collections;
using System.Threading;
using GoogleMobileAds.Api;
using UnityEngine;

public class BannerAd : MonoBehaviour {

	public enum SizeAd {

		SmartBanner,
		Banner,
		IabBanner,
		MediumRectangle

	}

	public SizeAd BannerSize;
	public AdPosition BannerPosition;

	[Range(10, 150)] public float RefreshDelay;
	public string AndroidBannerId, IphoneBannerId;
	private BannerView banner;
	private bool adLoading;
	private string bannerId;
	private AdPosition adPosition;
	private AdSize adSize;

	private void Start () {
#if UNITY_ANDROID
		bannerId = AndroidBannerId;
#elif UNITY_IPHONE
        bannerId = IphoneBannerId;
#else
		bannerId = "unexpected_platform";
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

		adLoading = true;
		LoadAd();
	}

	private void Update () {
		if (!adLoading) StartCoroutine(RefreshAd());
	}

	private void AdLoaded (object sender, EventArgs args) {
		adLoading = false;
		banner.OnAdLoaded -= AdLoaded;
		banner.OnAdFailedToLoad -= AdLoaded;
	}

	private IEnumerator RefreshAd () {
		adLoading = true;
		yield return new WaitForSeconds(RefreshDelay);

		LoadAd();
	}

	private void LoadAd () {
		banner = AdMobiManager.Instance.RequestBanner(
			bannerId,
			adSize,
			BannerPosition
		);

		banner.OnAdLoaded += AdLoaded;
		banner.OnAdFailedToLoad += AdLoaded;
	}

}