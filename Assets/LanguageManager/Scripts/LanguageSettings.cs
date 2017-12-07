using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public class LanguageSettings : MonoBehaviour {
    private List<SmartCultureInfo> availableLanguages;
    private LanguageManager languageManager;

    public SmartCultureInfo ActualLanguage;

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
            Debug.LogError(
                "No languages are created!, Open the Smart Localization plugin at Window->Smart Localization and create your language!");
        }

        if (deviceCulture != null) {
            SetLanguage(deviceCulture);
        } else {
            Debug.Log("The device language is not available in the current application. Loading default.");
            SetLanguage(availableLanguages[1]);
        }
    }

    private void SetLanguage (SmartCultureInfo language) {
        ActualLanguage = language;
        languageManager.ChangeLanguage(language);
    }

    public void SwitchLanguage () {
        int actual = ActualLanguage == availableLanguages[0] ? 1 : 0;
        SetLanguage(availableLanguages[actual]);
    }
}