using DG.Tweening;
using UnityEngine;

public class PulseEffect : MonoBehaviour {
    [Range(1f, 5f)] public float ApproachSpeed;
    [Range(1f, 2f)] public float GrowthBound;
    [Range(0f, 1f)] public float ShrinkBound;

    private bool finished = true;

    private void Update () {
        if (!finished) return;

        finished = false;

        Shrink();
    }

    private void Grow () {
        transform.DOScale(Vector3.one * GrowthBound, ApproachSpeed).OnComplete(() => finished = true);
    }

    private void Shrink () {
        transform.DOScale(Vector3.one * ShrinkBound, ApproachSpeed).OnComplete(Grow);
    }
}