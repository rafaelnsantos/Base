﻿using Facebook.Unity;
using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class HeaderLoggedIn : MonoBehaviour {

	private HighScore highScore;
	private Money money;
	public Text Welcome;
	public RawImage Picture;

	private static bool loaded;

	private void OnEnable () {
		FBAchievements.GetCompletedAchievements();
		FBAchievements.GetAchievements();
		GraphUtil.TargetingAd();
		
	}

	private void Awake () {
		highScore = GetComponentInChildren<HighScore>();
		money = GetComponentInChildren<Money>();
	}

	private void Start () {
		if (!loaded) {
			GameState.Load(finished => {
				UpdateUI();
				loaded = true;
			});
		} else {
			UpdateUI();
			GameState.Save();
		}
		LanguageManager.Instance.OnChangeLanguage += UpdateUI;
	}

	private void UpdateUI () {
		if (FacebookCache.UserTexture != null) {
			// Update Profile Picture
			Picture.enabled = true;
			Picture.texture = FacebookCache.UserTexture;
		} else {
			GraphUtil.LoadImgFromID(AccessToken.CurrentAccessToken.UserId, texture => {
				Picture.enabled = true;
				FacebookCache.UserTexture = texture;
				Picture.texture = texture;
			});
		}

		highScore.ChangeText();

		money.UpdateCoins();

//		if (FacebookCache.HighScore == null) {
//			CloudSave.GetInt("score", score => {
//				FacebookCache.HighScore = score;
//				ScoreText.text = LanguageManager.Instance.GetTextValue("Facebook.Score") + score;
//			});
//		} else {
//			ScoreText.text = LanguageManager.Instance.GetTextValue("Facebook.Score") + FacebookCache.HighScore;
//		}

		if (!string.IsNullOrEmpty(FacebookCache.Username)) {
			// Update Welcome Text
			Welcome.text = LanguageManager.Instance.GetTextValue("Facebook.Welcome") + FacebookCache.Username + "!";
		} else {
			FB.API("/me?fields=first_name", HttpMethod.GET, result => {
				string firstName;
				if (!result.ResultDictionary.TryGetValue("first_name", out firstName)) {
					return;
				}

				FacebookCache.Username = firstName;
				Welcome.text = LanguageManager.Instance.GetTextValue("Facebook.Welcome") + FacebookCache.Username + "!";
			});
		}
	}

	private void OnDestroy () {
		if (LanguageManager.HasInstance) LanguageManager.Instance.OnChangeLanguage -= UpdateUI;
	}

}