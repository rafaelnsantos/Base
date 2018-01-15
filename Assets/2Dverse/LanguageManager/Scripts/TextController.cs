using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	protected Text Text;
	public string Key;

	private LanguageManager languageManager;

	private void Awake () {
		Text = GetComponent<Text>();
		languageManager = LanguageManager.Instance;
	}

	private void Start () {
		ChangeText(languageManager);
		languageManager.OnChangeLanguage += ChangeText;
	}

	private void ChangeText (LanguageManager languageMan) {
		Text.text = languageMan.GetTextValue(Key);
	}

	private void OnDestroy () {
		languageManager.OnChangeLanguage -= ChangeText;
	}

}