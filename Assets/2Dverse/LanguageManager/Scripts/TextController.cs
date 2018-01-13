using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	protected Text Text;
	public string Key;

	private LanguageManager language;

	private void Awake () {
		Text = GetComponent<Text>();
		language = LanguageManager.Instance;
	}

	private void Start () {
		Debug.Log("text start");
		ChangeText(language);
		language.OnChangeLanguage += ChangeText;
	}

	private void ChangeText (LanguageManager languageMan) {
		Text.text = languageMan.GetTextValue(Key);
	}

	private void OnDestroy () {
		if (LanguageManager.HasInstance)
			// ReSharper disable once DelegateSubtraction
			language.OnChangeLanguage -= ChangeText;
	}

}