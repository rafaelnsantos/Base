using System;
using System.Collections.Generic;
using System.Globalization;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobSettings : ScriptableObject {

	public const string AssetName = "AdMobSettings";
	public const string Path = "2Dverse/Resources";
	public const string AssetExtension = ".asset";

	[SerializeField] private bool teste;
	[SerializeField] private string androidAppId;
	[SerializeField] private string iphoneAppId;
	[SerializeField] private bool noAds;
	[SerializeField] private bool tagForChild;

	public static bool Teste {
		get { return Instance.teste; }
		set { Instance.teste = value; }
	}

	public static bool TagForChild {
		get { return Instance.tagForChild; }
		set { Instance.tagForChild = value; }
	}

	public static string AndroidAppId {
		get { return Instance.androidAppId; }
		set { Instance.androidAppId = value; }
	}

	public static string IphoneAppId {
		get { return Instance.iphoneAppId; }
		set { Instance.iphoneAppId = value; }
	}

	public static bool NoAds {
		get { return Instance.noAds; }
		set { Instance.noAds = value; }
	}

	private string AppId {
		get {
#if UNITY_EDITOR || UNITY_WEBGL
			return "unexpected_platform";
#elif UNITY_IOS
        	return Teste ? "ca-app-pub-3940256099942544~1458002511" : IphoneAppId;
#elif UNITY_ANDROID
			return Teste ? "ca-app-pub-3940256099942544~3347511713" : AndroidAppId;
#endif
		}
	}

	private static AdMobSettings instance;

	public static AdMobSettings Instance {
		get {
			instance = NullableInstance ?? CreateInstance<AdMobSettings>();
			return instance;
		}
	}

	public static AdMobSettings NullableInstance {
		get { return instance ?? (instance = Resources.Load(AssetName) as AdMobSettings); }
	}

	public static AdRequest AdRequest {
		get {
			return Teste
				? new AdRequest.Builder()
					.AddTestDevice("8951c0025f5ad093")
					.Build()
				: new AdRequest.Builder()
					.TagForChildDirectedTreatment(Instance.tagForChild)
					.SetBirthday(FacebookCache.UserBirthday ?? new DateTime())
					.SetGender(FacebookCache.UserGender ?? Gender.Unknown)
					.Build();
		}
	}

	private void Start () {
		MobileAds.Initialize(AppId);
	}

}