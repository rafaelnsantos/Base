using SmartLocalization;
using UnityEngine;

public class HeaderNotLoggedIn : MonoBehaviour {

	private HighScore highScore;
	private Money money;

	public static bool loaded { get; private set; }

	private void Awake () {
		highScore = GetComponentInChildren<HighScore>();
		money = GetComponentInChildren<Money>();
	}

	private void Start () {
		if (!loaded) {
			GameState.Load(finished => {
				loaded = true;
				UpdateUI();
			});
		} else {
			GameState.Save();
			UpdateUI();
		}
		LanguageManager.Instance.OnChangeLanguage += UpdateUI;
	}

	private void UpdateUI () {
		highScore.ChangeText();

		money.UpdateCoins();
	}

}