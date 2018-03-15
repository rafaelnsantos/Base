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
		ChangeText();
		languageManager.OnChangeLanguage += ChangeText;
	}

	private void ChangeText () {
		if (Text != null) Text.text = languageManager.GetTextValue(Key);
	}

	private void OnDestroy () {
		if (LanguageManager.HasInstance) languageManager.OnChangeLanguage -= ChangeText;
	}

	public void SetKey (string key) {
		Key = key;
		ChangeText();
	}

	public string GetString () {
		return LanguageManager.Instance.GetTextValue(Key);
	}

}