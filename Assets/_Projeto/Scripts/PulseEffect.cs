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
		GrowX();

		GrowY();
	}

	private void GrowX () {
		if (!finishedX) return;

		finishedX = false;
		transform.DOScaleX(GrowthBound.x, ApproachSpeedX).OnComplete(ShrinkX);
	}

	private void ShrinkX () {
		transform.DOScaleX(ShrinkBound.x, ApproachSpeedX).OnComplete(() => finishedX = true);
	}

	private void GrowY () {
		if (!finishedY) return;

		finishedY = false;
		transform.DOScaleY(GrowthBound.y, ApproachSpeedY).OnComplete(GrowY);
	}

	private void ShrinkY () {
		transform.DOScaleY(ShrinkBound.y, ApproachSpeedY).OnComplete(() => finishedY = true);
	}

}