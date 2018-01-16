﻿using UnityEngine;

public class Touch : Pausable {

	protected override void PausableUpdate () {
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) ReadTouch();
#else
		if (Input.touchCount > 0) ReadTouch();
#endif
	}
	
	private void ReadTouch () {
		Vector2 touchPosition;

#if UNITY_EDITOR
		touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Check(touchPosition);
#else
		for (int i = 0; i < Input.touchCount; i++) {
			touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position); 
			Check(touchPosition);				
		}
#endif
	}

	//para verificar se o toque foi em algum gameObject com ITouchable
	private void Check (Vector2 touchPosition) {
		RaycastHit2D hitInformation = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 0);

		hitInformation.collider?.gameObject.GetComponent<ITouchable>()?.OnTouch();
	}

}