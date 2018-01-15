using UnityEngine;

public abstract class Savable : MonoBehaviour {

	private void Start () {
		Load();
	}

	protected abstract void Save ();

	protected abstract void Load ();

	private void OnApplicationQuit () {
		Save();
	}

	private void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) Save();
	}

}