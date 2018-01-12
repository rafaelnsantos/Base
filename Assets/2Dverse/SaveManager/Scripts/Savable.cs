using UnityEngine;

public abstract class Savable : MonoBehaviour {

	protected abstract void Save ();

	protected abstract void Load ();

	protected void OnApplicationQuit () {
		Save();
	}

	protected void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) Save();
	}

}