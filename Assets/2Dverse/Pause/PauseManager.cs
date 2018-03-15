using UnityEngine;

public class PauseManager : MonoBehaviour {
	public static PauseManager Instance { get; private set; }

	public delegate void OnPauseSwitch (bool isPaused);

	public event OnPauseSwitch HandlePauseSwitch;

	public GameObject pauseCanvas;

	private bool isPaused;

	private void Awake () {
		Instance = this;
	}

	public void PauseButton () {
		isPaused = !isPaused;
		
		Time.timeScale = isPaused ? 0 : 1;

		pauseCanvas.SetActive(isPaused);

		if (HandlePauseSwitch != null) HandlePauseSwitch(isPaused);
	}

}