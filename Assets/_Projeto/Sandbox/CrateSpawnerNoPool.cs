using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawnerNoPool : MonoBehaviour {

	public int MaxInstances;

	public GameObject Crate;
	public float Delay;

	public int Spawned = 0;

	void Start () {
		StartCoroutine(SpawnCrate());
	}

	IEnumerator SpawnCrate () {
		while (true) {
			yield return new WaitForSeconds(Delay);

			if (Spawned <= MaxInstances) {
				Instantiate(Crate, transform.position, Quaternion.identity);
				Spawned++;
			}
		}
	}

}