using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	protected Text text;
	public string Key;

	private void Awake () {
		text = GetComponent<Text>();
	}

	private void Start () {
		ChangeText(LanguageManager.Instance);
		LanguageManager.Instance.OnChangeLanguage += ChangeText;
	}

	protected void ChangeText (LanguageManager languageManager) {
		text.text = languageManager.GetTextValue(Key);
	}

	private void OnDestroy () {
		if (LanguageManager.HasInstance)
			LanguageManager.Instance.OnChangeLanguage -= ChangeText;
	}

}