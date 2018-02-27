using UnityEngine;
using UnityEngine.UI;

public class LoadSceneHelper : MonoBehaviour {

	public float WaitTime;
	public string SceneName;

	private void Start () {
		GetComponent<Button>().onClick.AddListener(LoadScene);
	}

	private void LoadScene () {
		SceneLoader.Instance.LoadScene(SceneName, WaitTime);
	}

}