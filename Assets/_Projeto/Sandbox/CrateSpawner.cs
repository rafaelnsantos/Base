using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour {

	private ObjectPooler cratePool;
	public float Delay;

	private void Awake () {
		cratePool = GetComponent<ObjectPooler>();
	}

	private void Start () {
		StartCoroutine(SpawnCrate());
	}

	IEnumerator SpawnCrate () {
		WaitForSeconds delay = new WaitForSeconds(Delay);
		while (true) {
			yield return delay;

			cratePool.GetPooledObject(transform.position, Quaternion.identity);
		}
	}

}