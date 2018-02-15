using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score Instance { get; private set; }

	private Text text;
	public int score { get; private set; }

	private void Awake () {
		text = GetComponent<Text>();
		Instance = this;
	}

	private void Start () {
		score = 0;
	}

	public void PlusOne () {
		score++;
		text.text = score.ToString();
	}

	private void OnDestroy () {
		if (FacebookCache.HighScore != null && score > FacebookCache.HighScore) {
			CloudSave.SetInt("score", score, saved => {
				if (saved) FacebookCache.HighScore = score;
			});
		}
	}

}