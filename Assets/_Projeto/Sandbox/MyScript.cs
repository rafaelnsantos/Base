using UnityEngine;

public class MyScript : Pausable {

	protected override void PausableUpdate () {
		Debug.Log("Running");
	}

}