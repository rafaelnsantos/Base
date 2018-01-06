using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdGameplay : MonoBehaviour {

	public AudioClip Musica;
	
	private void Start () {
		AdMobiManager.Instance.RequestBanner(AdSize.SmartBanner, AdPosition.Top);	
		AudioSettings.Instance.PlayMusic(Musica);
	}

	private void OnDestroy () {
		AdMobiManager.Instance.bannerView.Destroy();
	}
}
