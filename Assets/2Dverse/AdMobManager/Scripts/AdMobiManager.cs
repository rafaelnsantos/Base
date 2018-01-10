using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobiManager : MonoBehaviour {

	public string AndroidAppId;
	public string IphoneAppId;
	public bool Teste;

	public static AdMobiManager Instance { get; private set; }

	public AdRequest adRequest { get; private set; }

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
#elif UNITY_IOS
        string appId = Teste ? "ca-app-pub-3940256099942544~1458002511" : IphoneAppId;
#else
        string appId = "unexpected_platform";
#endif

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

		adRequest = CreateAdRequest();
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest () {
		return Teste
			? new AdRequest.Builder()
				.AddTestDevice("8951c0025f5ad093")
				.Build()
			: new AdRequest.Builder().Build();
	}

}