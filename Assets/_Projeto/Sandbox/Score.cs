using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score Instance { get; private set; }

	private Text text;
	private int score = 0;


	private void Awake () {
		text = GetComponent<Text>();
		Instance = this;
	}
	
	public void PlusOne () {
		score++;
		text.text = score.ToString();
	}

	
	

}