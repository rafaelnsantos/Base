using DG.Tweening;
using UnityEngine;

public class PulseEffect : MonoBehaviour {

	[Range(1f, 5f)] public float ApproachSpeedX;
	[Range(1f, 5f)] public float ApproachSpeedY;
	public Vector2 GrowthBound;
	public Vector2 ShrinkBound;

	private bool finishedX = true;
	private bool finishedY = true;

	private void Update () {
		ShrinkX();
		ShrinkY();
	}

	private void GrowX () {
		transform.DOScaleX(GrowthBound.x, ApproachSpeedX).OnComplete(() => finishedX = true);
	}

	private void ShrinkX () {
		if (!finishedX) return;

		finishedX = false;
		transform.DOScaleX(ShrinkBound.x, ApproachSpeedX).OnComplete(GrowX);
	}

	private void GrowY () {
		transform.DOScaleY(GrowthBound.y, ApproachSpeedY).OnComplete(() => finishedY = true);
	}

	private void ShrinkY () {
		if (!finishedY) return;

		finishedY = false;
		transform.DOScaleY(ShrinkBound.y, ApproachSpeedY).OnComplete(GrowY);
	}

}