using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader Instance { get; private set; }

    private Animator animator;

    public GameObject LoadingScreen;
    public float WaitTime;

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

    public void LoadScene (string sceneName) {
        LoadingScreen.SetActive(true);
        StartCoroutine(AsyncLoad(sceneName));
    }

    private IEnumerator AsyncLoad (string sceneName) {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(WaitTime);

        while (!async.isDone) {
            Debug.Log(async.progress * 100);
            if (async.progress < 0.90f) {
                //progressBar.fillAmount = async.progress / 0.9f;
                //percentage = async.progress * 100;
                //dst.text = (int)percentage + "%";
                //img.fillAmount= async.progress / 0.9f;
            } else {
                //img.fillAmount = async.progress / 0.9f;
                //percentage = (async.progress / 0.9f) * 100;
                //dst.text = (int)percentage + "%";
                async.allowSceneActivation = true;
            }
            yield return null;
        }

        animator.SetTrigger("FinishedLoading");
    }

}