using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public class LanguageSettings : Settings {

    private List<SmartCultureInfo> availableLanguages;
    private LanguageManager languageManager;
    

    private void Awake () {
        languageManager = LanguageManager.Instance;
    }

    private void Start () {
        availableLanguages = languageManager.GetSupportedLanguages();
        Load();
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
        int saved = SaveManager.GetInt("Language", 0);
        languageManager.ChangeLanguage(availableLanguages[saved]);
    }
    
}