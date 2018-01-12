using UnityEngine;

public abstract class Pausable : MonoBehaviour {

	private bool paused;

	protected void Start () {
		PauseButton.Instance.OnPause += HandlePause;
		paused = PauseButton.Instance.Paused;
	}

	private void Update () {
		if (paused) return;
		PausableUpdate();
	}

	private void HandlePause (bool pause) {
		paused = pause;
	}

	protected abstract void PausableUpdate ();

	protected void OnDestroy () {
		PauseButton.Instance.OnPause -= HandlePause;
	}

}