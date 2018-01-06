using UnityEngine;

public abstract class Settings : MonoBehaviour {
    protected abstract void Save ();

    protected abstract void Load ();

    protected void OnApplicationQuit () {
        Save();
    }
    
    protected void OnApplicationPause (bool pauseStatus) {
        if (pauseStatus) Save();
    }


}