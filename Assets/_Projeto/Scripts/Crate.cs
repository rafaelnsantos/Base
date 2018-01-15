﻿using UnityEngine;

public class Crate : MonoBehaviour, ITouchable {

	private Score score;

	private void Awake () {
		score = Score.Instance;
		Debug.Log("awake");
	}

	public void OnTouch () {
		gameObject.SetActive(false);
		score.PlusOne();
	}

}
