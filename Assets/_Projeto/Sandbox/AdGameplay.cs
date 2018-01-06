using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdGameplay : MonoBehaviour {

	private void Start () {
		AdMobiManager.Instance.RequestBanner(AdSize.SmartBanner, AdPosition.Top);	
	}

	private void OnDestroy () {
		AdMobiManager.Instance.bannerView.Destroy();
	}
}
