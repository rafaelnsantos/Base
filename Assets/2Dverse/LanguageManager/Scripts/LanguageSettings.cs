using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public class LanguageSettings : MonoBehaviour, ISettings {

    private List<SmartCultureInfo> availableLanguages;
    private LanguageManager languageManager;
    private SmartCultureInfo actualLanguage;

    public static LanguageSettings Instance { get; private set; }

    private void Awake () {
        // Singleton Patter
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        languageManager = LanguageManager.Instance;
    }

    private void Start () {
        SmartCultureInfo deviceCulture = languageManager.GetDeviceCultureIfSupported();

        if (languageManager.NumberOfSupportedLanguages > 0) {
            availableLanguages = languageManager.GetSupportedLanguages();
        }

        if (deviceCulture != null) {
            SetLanguage(deviceCulture);
        } else {
            Load();
        }
    }

    private void SetLanguage (SmartCultureInfo language) {
        actualLanguage = language;
        languageManager.ChangeLanguage(language);
    }

    public void SwitchLanguage () {
        int actual = actualLanguage == availableLanguages[0] ? 1 : 0;
        SetLanguage(availableLanguages[actual]);
    }

    public void Save () {
        int actual = actualLanguage == availableLanguages[0] ? 0 : 1;
        SaveManager.SetInt("Language", actual);
    }

    public void Load () {
        int saved = SaveManager.GetInt("Language", 0);
        SetLanguage(availableLanguages[saved]);
    }

    private void OnApplicationPause (bool pauseStatus) {
        if (pauseStatus) {
            Save();
        }
    }

}