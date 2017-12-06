using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateEffect : MonoBehaviour {
    [Range(5f, 200f)] public float Speed;
    public bool clockwise;

    private void Update () {
        transform.Rotate(clockwise ? Vector3.back : Vector3.forward, Speed * Time.deltaTime);
    }
}