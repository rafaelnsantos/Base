using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	private Text text;
	public string Key;

	private void Awake () {
		text = GetComponent<Text>();
	}
	
	private void Start () {
		text.text = LanguageManager.Instance.GetTextValue(Key);
		LanguageManager.Instance.OnChangeLanguage += OnLanguageChanged;
	}
	
	private void OnLanguageChanged (LanguageManager languageManager) {
		text.text = languageManager.GetTextValue(Key);
	}
	
	private void OnDestroy () {
		if (LanguageManager.HasInstance) {
			LanguageManager.Instance.OnChangeLanguage -= OnLanguageChanged;
		}
	}
}
