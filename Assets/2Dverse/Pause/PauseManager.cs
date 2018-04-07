using System;
using UnityEngine;

public class PauseManager : MonoBehaviour {
	public static PauseManager Instance { get; private set; }

	public GameObject pauseCanvas;

	public static Action<bool> onPause;

	private bool isPaused;

	private void Awake () {
		Instance = this;
	}

	public void PauseButton () {
		isPaused = !isPaused;
		
		Time.timeScale = isPaused ? 0 : 1;

		pauseCanvas.SetActive(isPaused);

		if (onPause != null) onPause(isPaused);
	}

}