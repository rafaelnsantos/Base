using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public class LanguageSettings : MonoBehaviour {
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
        } else {
            Debug.LogError("Open the Smart Localization plugin and create your language!");
        }

        if (deviceCulture != null) {
            SetLanguage(deviceCulture);
        } else {
            Debug.Log("The device language is not available in the current application. Loading portuguese.");
            SetLanguage(availableLanguages[1]);
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
}