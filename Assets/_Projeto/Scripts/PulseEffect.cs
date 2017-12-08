using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PulseEffect : MonoBehaviour {
    [Range(0.1f, 5f)] public float ApproachSpeed = 0.02f;
    [Range(1f, 5f)] public float GrowthBound = 2f;
    [Range(0f, 1f)] public float ShrinkBound = 0.5f;

    private bool go = true;

    private void Update () {
        if (!go) return;

        go = false;

        Shrink();
    }

    private void Grow () {
        transform.DOScale(Vector3.one * GrowthBound, ApproachSpeed).OnComplete(() => go = true);
    }

    private void Shrink () {
        transform.DOScale(Vector3.one * ShrinkBound, ApproachSpeed).OnComplete(Grow);
    }
}