using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	private Button button;

	private bool paused;
	
	public static PauseButton Instance { get; private set; }

	public delegate void OnPauseSwitch (bool paused);
	public event OnPauseSwitch OnPause;

	private void Awake () {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(Switch);
		SceneLoader.Instance.OnSceneLoad += ReturnTimeScale;
	}

	private void ReturnTimeScale () {
		Time.timeScale = 1;
	}

	private void Switch () {
		paused = !paused;
		Time.timeScale = paused ? 0 : 1;
		OnPause?.Invoke(paused);
	}

	private void OnDestroy () {
		SceneLoader.Instance.OnSceneLoad -= ReturnTimeScale;
	}

}
