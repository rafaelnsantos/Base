﻿using System.Collections.Generic;
using SmartLocalization;

public class LanguageSettings : Savable {

	private List<SmartCultureInfo> availableLanguages;
	private LanguageManager languageManager;

	private void Awake () {
		languageManager = LanguageManager.Instance;
		availableLanguages = languageManager.GetSupportedLanguages();
	}

	public void SwitchLanguage () {
		int actual = languageManager.CurrentlyLoadedCulture == availableLanguages[0] ? 1 : 0;
		languageManager.ChangeLanguage(availableLanguages[actual]);
		Save();
	}

	protected override void Save () {
		int actual = languageManager.CurrentlyLoadedCulture == availableLanguages[0] ? 0 : 1;
		SaveManager.SetInt("Language", actual);
	}

	protected override void Load () {
		int saved = SaveManager.GetInt("Language", 1);
		languageManager.ChangeLanguage(availableLanguages[saved]);
	}

}