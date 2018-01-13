using GoogleMobileAds.Api;
using UnityEngine;

public class AdInterstitial : MonoBehaviour {

	public string AndroidId, IphoneId;

	private string InterstitialId {
		get {
#if UNITY_EDITOR
			return "unexpected_platform";
#elif UNITY_IOS
      		return AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/4411468910" : IphoneId;
#elif UNITY_ANDROID
			return AdMobiManager.Instance.Teste ? "ca-app-pub-3940256099942544/1033173712" : AndroidId;
#endif
		}
	}

	private InterstitialAd interstitialAd;

	private void Start () {
		if (AdMobiManager.Instance.NoAds) return;

		SceneLoader.Instance.OnSceneLoad += RequestInterstitial;
		SceneLoader.Instance.OnLoadedScene += ShowAds;
	}

	private void RequestInterstitial () {
		// Clean up interstitial ad before creating a new one.
		interstitialAd?.Destroy();

		// Create an interstitial.
		interstitialAd = new InterstitialAd(InterstitialId);

		// Load an interstitial ad.
		interstitialAd.LoadAd(AdMobiManager.Instance.AdRequest);
	}

	private void ShowAds () {
		if (interstitialAd.IsLoaded()) interstitialAd.Show();
	}

	private void OnDestroy () {
		if (AdMobiManager.Instance.NoAds) return;

		SceneLoader.Instance.OnSceneLoad -= RequestInterstitial;
		SceneLoader.Instance.OnLoadedScene -= ShowAds;
	}

}