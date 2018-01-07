using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnStartScene : MonoBehaviour {

	public AudioClip Music;

	void Start () {
		AudioSettings.Instance.PlayMusic(Music);
	}
	
}
