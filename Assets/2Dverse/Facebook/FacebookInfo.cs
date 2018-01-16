using System.Collections.Generic;
using UnityEngine;

public class FacebookInfo : MonoBehaviour {

	public static Texture UserTexture;
	public static string Username;
	public static List<object> Friends;
	public static Dictionary<string, Texture> FriendImages = new Dictionary<string, Texture>();
	private int? highScore;
	public static int Score { get; set; }
	public static bool ScoresReady;
	private static List<object> scores;

	public static List<object> Scores {
		get { return scores; }
		set {
			scores = value;
			ScoresReady = true;
		}
	}

	public static int HighScore {
		get { return Instance.highScore ?? 0; }
		set { Instance.highScore = value; }
	}

	public static FacebookInfo Instance { get; private set; }

	private void Awake () {
		// Singleton Pattern
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	public static void CallUIRedraw () {
		GameObject.Find("FacebookManager")?.GetComponent<FacebookManager>()?.RedrawUI();
	}

}