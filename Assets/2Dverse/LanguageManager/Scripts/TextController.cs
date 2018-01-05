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
		ChangeText(LanguageManager.Instance);
		LanguageManager.Instance.OnChangeLanguage += ChangeText;
	}
	
	private void ChangeText (LanguageManager languageManager) {
		text.text = languageManager.GetTextValue(Key);
	}
	
	private void OnDestroy () {
		if(LanguageManager.Instance)
			LanguageManager.Instance.OnChangeLanguage -= ChangeText;
	}
}
