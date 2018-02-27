using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public static class FacebookCache {

	public static Texture UserTexture;
	public static string Username;
	public static Dictionary<string, string> ScoreNames = new Dictionary<string, string>();
	public static Dictionary<string, Texture> ScoreImages = new Dictionary<string, Texture>();
	public static int? HighScore;
	public static int Score { get; set; }

	public static List<object> CompletedAchievements;

	public static List<object> Achievements;
	public static Dictionary<string, Texture> AchievementsImages = new Dictionary<string, Texture>();
	public static string ServerURL = "http://2dversestudio.com.br/";

	public static DateTime? UserBirthday = null;
	public static Gender? UserGender = null;

}