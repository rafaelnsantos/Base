using System.Collections;
using UnityEngine;

public class MusicOnStartScene : MonoBehaviour {

	public AudioClip Music;
    public float Delay;

	private void Start () {
		StartCoroutine(PlayMusic());
	}

	private IEnumerator PlayMusic () {
		yield return new WaitForSeconds(Delay);
		AudioManager.PlayMusic(Music);
	}
	
}
