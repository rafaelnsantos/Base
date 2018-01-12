using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Move : MonoBehaviour {

	public Vector2 A, B;

	public float Time;

	private bool move = true;


	private void Update () {
		if (move) MoveToA();
	}

	private void MoveToA () {
		move = false;
		transform.DOLocalMove(A, Time).OnComplete(MoveToB);
	}

	private void MoveToB () {
		transform.DOLocalMove(B, Time).OnComplete(() => move = true);
	}

}