using UnityEngine;

public abstract class SavableScript : ScriptableObject {

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