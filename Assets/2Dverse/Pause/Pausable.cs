using UnityEngine;

/// <summary>
/// Desativa o script quando o jogo pausa
/// </summary>
public abstract class Pausable : MonoBehaviour {
	private void Start () {
		PauseManager.Instance.HandlePauseSwitch += HandlePause;
	}

	private void HandlePause (bool ispaused) {
		enabled = !ispaused;
	}

	private void OnDestroy () {
		PauseManager.Instance.HandlePauseSwitch -= HandlePause;
	}
}