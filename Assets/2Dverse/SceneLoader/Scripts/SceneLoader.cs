using System;
using System.Collections;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public static SceneLoader Instance { get; private set; }
	private Animator animator;
	public GameObject LoadingScreen;
	public Slider ProgressSlider;
	public Text ProgressText;

	private bool loadingScene;

	public delegate void OnSceneLoading ();

	public event OnSceneLoading OnSceneLoad;

	public delegate void OnSceneLoaded ();

	public event OnSceneLoaded OnLoadedScene;

	private void Awake () {
		// Singleton Pattern
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		animator = LoadingScreen.GetComponent<Animator>();
	}

	public void LoadScene (string sceneName, float waitTime = 0) {
		if (loadingScene) return;

		loadingScene = true;
		Time.timeScale = 1;
		if (OnSceneLoad != null) OnSceneLoad();
		AudioManager.StopMusic();
		LoadingScreen.SetActive(true);
		GC.Collect();
		StartCoroutine(waitTime > 0 ? AsyncLoadWorkAround(sceneName, waitTime) : AsyncLoad(sceneName));
	}

	private IEnumerator AsyncLoad (string sceneName) {
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
		async.allowSceneActivation = false;

		while (!async.isDone) {
			float progress = Mathf.Clamp01(async.progress / .9f);
			ProgressSlider.value = progress;
			ProgressText.text = String.Concat((int) (progress * 100), "%");
			if (async.progress >= 0.9f) {
				if (OnLoadedScene != null) OnLoadedScene();
				async.allowSceneActivation = true;
				animator.SetTrigger("FinishedLoading");
				loadingScene = false;
			}
			yield return null;
		}
	}

	private IEnumerator AsyncLoadWorkAround (string sceneName, float waitTime) {
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
		async.allowSceneActivation = false;
		float timer = 0;

		while (timer <= waitTime) {
			float progress = Mathf.Clamp01(timer / waitTime);
			ProgressSlider.value = progress;
			ProgressText.text = String.Concat((int) (progress * 100), "%");
			timer += Time.deltaTime;
			yield return null;
		}

		if (OnLoadedScene != null) OnLoadedScene();
		async.allowSceneActivation = true;
		animator.SetTrigger("FinishedLoading");
		ProgressSlider.value = 1;
		ProgressText.text = "100%";
		loadingScene = false;
	}
	
	private void OnApplicationPause (bool pauseStatus) {
		if (!pauseStatus && FB.IsInitialized) FB.ActivateApp();
	}

}