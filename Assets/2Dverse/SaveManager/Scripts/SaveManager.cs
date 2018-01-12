using System;
using UnityEngine;

public static class SaveManager {

	public static void SetInt (string key, int value) {
		PlayerPrefs.SetInt(key, value);
	}

	public static int GetInt (string key, int defaultValue = 0) {
		return PlayerPrefs.GetInt(key, defaultValue);
	}

	public static void SetBool (string key, bool value) {
		PlayerPrefs.SetInt(key, Convert.ToInt32(value));
	}

	public static bool GetBool (string key, bool defaultValue = false) {
		return Convert.ToBoolean(PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
	}

	public static void SetFloat (string key, float value) {
		PlayerPrefs.SetFloat(key, value);
	}

	public static float GetFloat (string key, float defaultValue = 0f) {
		return PlayerPrefs.GetFloat(key, defaultValue);
	}

	public static void SetString (string key, string value) {
		PlayerPrefs.SetString(key, value);
	}

	public static string GetString (string key) {
		string defaultValue = "";
		return PlayerPrefs.GetString(key, defaultValue);
	}

}