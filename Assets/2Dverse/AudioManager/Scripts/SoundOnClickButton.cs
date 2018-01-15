using UnityEngine;
using UnityEngine.UI;

public class SoundOnClickButton : MonoBehaviour {

	public AudioClip Audio;

	private void Start () {
		GetComponent<Button>().onClick.AddListener(PlayClick);
	}

	private void PlayClick () {
		AudioManager.Instance.PlayEffect(Audio);
	}

}