using UnityEngine;

public class Teste : MonoBehaviour {

	private AudioManager audioManager;
	public AudioClip AudioClip;

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.Instance;
	}

	// Update is called once per frame
	void Update () {
		audioManager.PlayMusic(AudioClip);
	}
}