using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour {

	public enum Languages {

		English = 0,
		Portuguese = 1

	}
	
	private LanguageManager languageManager;
	private Image image;
	
	public Languages Language;

	private void Start () {
		languageManager.OnChangeLanguage += LanguageChanged;
		LanguageChanged();
	}

	private void Awake () {
		GetComponent<Button>().onClick.AddListener(SwitchLanguage);
		languageManager = LanguageManager.Instance;
		image = GetComponent<Image>();
	}

	private void SwitchLanguage () {
		LanguageSettings.ChangeLanguage((int) Language);
	}

	private void LanguageChanged () {
		image.color = new Color(1, 1, 1, LanguageSettings.ActivatedLanguage == (int) Language ? 1f : 0.5f);
	}
	
	private void OnDestroy () {
		if (LanguageManager.HasInstance) languageManager.OnChangeLanguage -= LanguageChanged;
	}

}