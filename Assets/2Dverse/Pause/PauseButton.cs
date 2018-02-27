using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	private Button button;
	public bool Paused { get; private set; }

	public static PauseButton Instance { get; private set; }

	public delegate void OnPauseSwitch (bool paused);

	public event OnPauseSwitch OnPause;

	public GameObject PausePanel;

	private void Awake () {
		Instance = this;
	}

	private void Start () {
		GetComponent<Button>().onClick.AddListener(Switch);
		SceneLoader.Instance.OnSceneLoad += ReturnTimeScale;
	}

	private void ReturnTimeScale () {
		Time.timeScale = 1;
	}

	public void Switch () {
		Paused = !Paused;
		PausePanel.SetActive(Paused);
		Time.timeScale = Paused ? 0 : 1;
		if (OnPause != null) OnPause(Paused);
	}

	private void OnDestroy () {
		SceneLoader.Instance.OnSceneLoad -= ReturnTimeScale;
	}

}