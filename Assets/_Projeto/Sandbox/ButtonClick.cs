using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour {

	public AudioClip Audio;

	public void PlayClick () {
		AudioManager.Instance.PlayEffect(Audio);
	}
}
