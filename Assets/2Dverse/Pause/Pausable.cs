using UnityEngine;

public abstract class Pausable : MonoBehaviour {
	private void Start () {
		PauseManager.onPause += HandlePause;
	}

	private void HandlePause (bool ispaused) {
		enabled = !ispaused;
	}

	private void OnDestroy () {
		PauseManager.onPause -= HandlePause;
	}
}