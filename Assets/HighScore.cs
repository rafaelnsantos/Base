using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	private Text scoreText;
	
	public int highScore { get; private set; }

	public string localizationKey;
	public string cloudSaveKey;

	private void Awake () {
		scoreText = GetComponent<Text>();
	}

	private void Start () {
		LanguageManager.Instance.OnChangeLanguage += ChangeText;
		ChangeText();
	}

	public void ChangeText () {
		scoreText.text = LanguageManager.Instance.GetTextValue(localizationKey) + GameState.GetInt(cloudSaveKey);
	}

	private void OnDestroy () {
		if(LanguageManager.HasInstance) LanguageManager.Instance.OnChangeLanguage -= ChangeText;
	}

}