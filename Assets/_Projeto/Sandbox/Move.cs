using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Move : MonoBehaviour {

	private bool finishedX = true;
	private bool finishedY = true;

	private void Update () {
		MoveRight();
		MoveUp();
	}

	private void MoveRight () {
		if (!finishedX) return;
		finishedX = false;
		transform.DOLocalMoveX(430, 3).OnComplete(MoveLeft);
	}

	private void MoveLeft () {
		transform.DOLocalMoveX(-430, 3).OnComplete(() => finishedX = true);
	}
	
	private void MoveUp() {
		if (!finishedY) return;
		finishedY = false;
		transform.DOLocalMoveY(220, 3).OnComplete(MoveDown);
	}

	private void MoveDown () {
		transform.DOLocalMoveY(-220, 3).OnComplete(() => finishedY = true);
	}

}