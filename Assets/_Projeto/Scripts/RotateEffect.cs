using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateEffect : MonoBehaviour {
    [Range(5f, 200f)] public float Speed;
    public bool Clockwise;

    private void Update () {
        transform.Rotate(Clockwise ? Vector3.back : Vector3.forward, Speed * Time.deltaTime);
    }
}