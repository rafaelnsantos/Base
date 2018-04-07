using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	private Text scoreText;
	
	public int highScore { get; private set; }

	public string localizationKey;
	public string cloudSaveKey;


	private void OnEnable () {
		GameState.onLoad += ChangeText;
		LanguageManager.Instance.OnChangeLanguage += ChangeText;

	}

	private void Awake () {
		scoreText = GetComponent<Text>();
	}

	public void ChangeText () {
		scoreText.text = LanguageManager.Instance.GetTextValue(localizationKey) + GameState.GetInt(cloudSaveKey);
	}

	private void OnDisable () {
		GameState.onLoad -= ChangeText;
		if(LanguageManager.HasInstance) LanguageManager.Instance.OnChangeLanguage -= ChangeText;
	}

}