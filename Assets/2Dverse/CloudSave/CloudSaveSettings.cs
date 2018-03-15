using UnityEngine;

public class CloudSaveSettings : ScriptableObject {

	public const string AssetName = "CloudSaveSettings";
	public const string Path = "2Dverse/CloudSave/Resources";
	public const string AssetExtension = ".asset";

	[SerializeField] private string url;
	[SerializeField] private string key;
	[SerializeField] private bool encrypted;

	public static string URL {
		get { return Instance.url; }
		set { Instance.url = value; }
	}

	public static string Key {
		get { return Instance.key; }
		set { Instance.key = value; }
	}

	public static bool Encrypted {
		get { return Instance.encrypted; }
		set { Instance.encrypted = value; }
	}

	private static CloudSaveSettings instance;

	public static CloudSaveSettings Instance {
		get {
			instance = NullableInstance ?? CreateInstance<CloudSaveSettings>();
			return instance;
		}
	}

	public static CloudSaveSettings NullableInstance {
		get { return instance ?? (instance = Resources.Load(AssetName) as CloudSaveSettings); }
	}

}