using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdGameplay : MonoBehaviour {

	public AdPosition PositionAd;

	[Range(10, 60)] public float RefreshDelay;
	public string AndroidBannerId, IphoneBannerId;
	private BannerView banner;
	private bool adLoading;
	private string bannerId;
	private AdPosition adPosition;

	private void Start () {
#if UNITY_ANDROID
        bannerId = AndroidBannerId;
#elif UNITY_IPHONE
        bannerId = IphoneBannerId;
#else
		bannerId = "unexpected_platform";
#endif
		adLoading = true;
		LoadAd();
	}

	private void Update () {
		if (!adLoading) StartCoroutine(RefreshAd());
	}

	private void OnDestroy () {
		if (banner != null) banner.Destroy();
	}

	private void AdLoaded (object sender, EventArgs args) {
		adLoading = false;
		banner.OnAdLoaded -= AdLoaded;
		banner.OnAdFailedToLoad -= AdLoaded;
	}
	
	
	private void AdLoadFailed (object sender, EventArgs args) {
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
			AdSize.SmartBanner,
			PositionAd
		);

		banner.OnAdLoaded += AdLoaded;
		banner.OnAdFailedToLoad += AdLoadFailed;
	}

}