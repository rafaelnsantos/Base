using UnityEngine;

public class ButtonClick : MonoBehaviour {

	public AudioClip Audio;

	public void PlayClick () {
		AudioManager.Instance.PlayEffect(Audio);
	}
}
