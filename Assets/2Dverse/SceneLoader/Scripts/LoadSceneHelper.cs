using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneHelper : MonoBehaviour {

	public float WaitTime;
	public bool WorkAround;

	public void LoadScene (string sceneName) {
		AdMobiManager.Instance.DestroyBanner();
		AudioSettings.Instance.StopMusic();
		if (WorkAround) {
			SceneLoader.Instance.LoadSceneWorkAround(sceneName, WaitTime);
		} else {
			SceneLoader.Instance.LoadScene(sceneName);
		}
	}

}