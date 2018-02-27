using System.Collections.Generic;
using SmartLocalization;
using UnityEngine;

public class LanguageSettings : MonoBehaviour {

	public static List<SmartCultureInfo> AvailableLanguages {
		get { return LanguageManager.Instance.GetSupportedLanguages(); }
	}

	public static int ActivatedLanguage { get; private set; }

	private void Awake () {
		int saved = SaveManager.GetInt("Language", 1);
		ActivatedLanguage = saved;
		LanguageManager.Instance.ChangeLanguage(AvailableLanguages[saved]);
	}

	public static void ChangeLanguage (int language) {
		ActivatedLanguage = language;
		LanguageManager.Instance.ChangeLanguage(AvailableLanguages[language]);
		SaveManager.SetInt("Language", language);
	}

}