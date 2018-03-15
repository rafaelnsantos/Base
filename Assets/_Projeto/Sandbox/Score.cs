using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score Instance { get; private set; }

	private Text text;
	public int score { get; private set; }

	private int highScore;
	
	public int HighScore {
		get { return Instance.highScore; }
		private set { Instance.highScore = value; }
	}

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
		if (score > GameState.GetInt("score")) {
			GameState.SetInt("score", score);
		}
	}

}