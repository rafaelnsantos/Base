using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader Instance { get; private set; }

    private Animator animator;

    public GameObject LoadingScreen;

    public Slider ProgressSlider;
    public Text ProgressText;
    
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

    public void LoadSceneWorkAround (string sceneName, float waitTime) {
        LoadingScreen.SetActive(true);
        StartCoroutine(AsyncLoadWorkAround(sceneName, waitTime));
    }
    
    public void LoadScene (string sceneName) {
        LoadingScreen.SetActive(true);
        StartCoroutine(AsyncLoad(sceneName));
    }

    private IEnumerator AsyncLoad (string sceneName) {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (!async.isDone) {
            float progress = Mathf.Clamp01(async.progress / .9f);
            ProgressSlider.value = progress;
            ProgressText.text = String.Concat((int)(progress * 100), "%");
            if (async.progress < 0.90f) {
            } else {
                animator.SetTrigger("FinishedLoading");
                async.allowSceneActivation = true;
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
            ProgressText.text = String.Concat((int)(progress * 100), "%");
            timer += Time.deltaTime;
            yield return null;
        }
        
        animator.SetTrigger("FinishedLoading");
        async.allowSceneActivation = true;
    }
}