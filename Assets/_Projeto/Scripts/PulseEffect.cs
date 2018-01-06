using DG.Tweening;
using UnityEngine;

public class PulseEffect : MonoBehaviour {

	[Range(1f, 5f)] public float ApproachSpeed;
	public Vector2 GrowthBound;
	public Vector2 ShrinkBound;

	private bool finished = true;

	private void Update () {
		if (!finished) return;

		finished = false;

		Shrink();
	}

	private void Grow () {
		transform.DOScale(GrowthBound, ApproachSpeed).OnComplete(() => finished = true);
	}

	private void Shrink () {
		transform.DOScale(ShrinkBound, ApproachSpeed).OnComplete(Grow);
	}

}