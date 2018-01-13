using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobiManager : Savable {

	public bool Teste;
	public string AndroidAppId;
	public string IphoneAppId;

	public bool NoAds;

	private string AppId {
		get {
#if UNITY_EDITOR
			return "unexpected_platform";
#elif UNITY_IOS
        	return Teste ? "ca-app-pub-3940256099942544~1458002511" : IphoneAppId;
#elif UNITY_ANDROID
			return Teste ? "ca-app-pub-3940256099942544~3347511713" : AndroidAppId;
#endif
		}
	}

	public static AdMobiManager Instance { get; private set; }

	public AdRequest AdRequest => Teste
		? new AdRequest.Builder()
			.AddTestDevice("8951c0025f5ad093")
			.Build()
		: new AdRequest.Builder().Build();

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
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(AppId);
	}

	protected override void Save () {
		SaveManager.SetBool("noads", NoAds);
	}

	protected override void Load () {
		NoAds = SaveManager.GetBool("noads");
	}

}