using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : Pausable {

	private new void Start () {
		base.Start();
		Debug.Log("asd");
		// ...
	}

	protected override void PausableUpdate () {
		Debug.Log("Running");
	}

	private new void OnDestroy () {
		base.OnDestroy();
		// ...
	}

}
