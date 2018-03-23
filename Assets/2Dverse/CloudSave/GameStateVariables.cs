using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateVariables : ScriptableObject {

	public const string AssetName = "GameStateVariables";
	public const string Path = "2Dverse/CloudSave/Resources";
	public const string AssetExtension = ".asset";

	[SerializeField] private List<string> stringKeys;
	[SerializeField] private List<string> intKeys;
	[SerializeField] private List<string> boolKeys;
	[SerializeField] private List<string> floatKeys;
	
	public static List<string> StringKeys {
		get { return Instance.stringKeys; }
		set { Instance.stringKeys = value; }
	}
	
	public static List<string> IntKeys {
		get { return Instance.intKeys; }
		set { Instance.intKeys = value; }
	}
	
	public static List<string> BoolKeys {
		get { return Instance.boolKeys; }
		set { Instance.boolKeys = value; }
	}
	
	public static List<string> FloatKeys {
		get { return Instance.floatKeys; }
		set { Instance.floatKeys = value; }
	}
	
	private static GameStateVariables instance;

	public static GameStateVariables Instance {
		get {
			instance = NullableInstance ?? CreateInstance<GameStateVariables>();
			return instance;
		}
	}

	public static GameStateVariables NullableInstance {
		get { return instance ?? (instance = Resources.Load(AssetName) as GameStateVariables); }
	}

}