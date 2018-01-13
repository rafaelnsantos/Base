using UnityEngine;

public abstract class Pausable : MonoBehaviour {

	private bool paused;
	private IPausable pausableImplementation;

	private void Start () {
		PauseButton.Instance.OnPause += HandlePause;
		paused = PauseButton.Instance.Paused;
	}

	private void Update () {
		if (paused) return;

		PausableUpdate();
	}

	private void FixedUpdate () {
		if (paused) return;

		PausableFixedUpdate();
	}

	private void LateUpdate () {
		if (paused) return;

		PausableLateUpdate();
	}

	private void HandlePause (bool pause) {
		paused = pause;
	}

	protected virtual void PausableLateUpdate () { }
	protected virtual void PausableFixedUpdate () { }
	protected virtual void PausableUpdate () { }

	private void OnDestroy () {
		PauseButton.Instance.OnPause -= HandlePause;
	}

}