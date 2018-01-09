using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdGameplay : MonoBehaviour {

	public float Delay;
	public string AndroidBannerId, IphoneBannerId;
	private BannerView banner;
	private bool adLoading;
	private string bannerId;

	private void Start () {
#if UNITY_EDITOR
		bannerId = "unused";
#elif UNITY_ANDROID
        bannerId = AndroidBannerId;
#elif UNITY_IPHONE
        bannerId = IphoneBannerId;
#endif
	}

	private void Update () {
		if (!adLoading) StartCoroutine(LoadAd());
	}

	private void OnDestroy () {
		banner.Destroy();
	}

	private void AdLoaded (object sender, EventArgs args) {
		adLoading = false;
		banner.OnAdLoaded -= AdLoaded;
	}

	private IEnumerator LoadAd () {
		adLoading = true;
		yield return new WaitForSeconds(Delay);

		banner = AdMobiManager.Instance.RequestBanner(bannerId, AdSize.SmartBanner, AdPosition.Top);
		banner.OnAdLoaded += AdLoaded;
	}

}