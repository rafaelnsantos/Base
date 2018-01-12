using UnityEngine;

public abstract class Pausable : MonoBehaviour {

	private bool paused;

	protected void Start () {
		PauseButton.Instance.OnPause += HandlePause;
	}

	private void Update () {
		if (paused) return;
		PausableUpdate();
	}

	private void HandlePause (bool pause) {
		paused = pause;
	}

	protected abstract void PausableUpdate ();

	private void OnDestroy () {
		PauseButton.Instance.OnPause -= HandlePause;
	}

}