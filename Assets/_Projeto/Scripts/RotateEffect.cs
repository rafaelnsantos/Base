using UnityEngine;

public class RotateEffect : MonoBehaviour {

	[Range(5f, 200f)] public float Speed;
	public bool Clockwise = true;

	private void Update () {
		transform.Rotate(Clockwise ? Vector3.back : Vector3.forward, Speed * Time.deltaTime);
	}

}