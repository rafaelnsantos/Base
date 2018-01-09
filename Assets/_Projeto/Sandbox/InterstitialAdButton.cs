using UnityEngine;
using UnityEngine.UI;

public class InterstitialAdButton : MonoBehaviour {

	public string AndroidId, IphoneId;

	private string interstitialId;

	private Button button;

	private void Awake () {
		button = GetComponent<Button>();
	}

	private void Start () {
#if UNITY_ANDROID
		interstitialId = AndroidId;
#elif UNITY_IPHONE
        interstitialId = IphoneId;
#else
		interstitialId = "unexpected_platform";
#endif

		button.onClick.AddListener(LoadAd);
	}

	private void LoadAd () {
		AdMobiManager.Instance.RequestInterstitial(interstitialId);
	}

}