using UnityEngine;

public abstract class Pausable : MonoBehaviour {

	protected bool Paused;

	private void Start () {
		PauseButton.Instance.OnPause += HandlePause;
		Paused = PauseButton.Instance.Paused;
	}

	private void Update () {
		if (Paused) return;

		PausableUpdate();
	}

	private void FixedUpdate () {
		if (Paused) return;

		PausableFixedUpdate();
	}

	private void LateUpdate () {
		if (Paused) return;

		PausableLateUpdate();
	}

	private void HandlePause (bool pause) {
		Paused = pause;
	}

	protected virtual void PausableLateUpdate () { }
	protected virtual void PausableFixedUpdate () { }
	protected virtual void PausableUpdate () { }

	private void OnDestroy () {
		PauseButton.Instance.OnPause -= HandlePause;
	}

}