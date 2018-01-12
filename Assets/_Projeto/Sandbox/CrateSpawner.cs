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
		while (true) {
			yield return new WaitForSeconds(Delay);

			cratePool.GetPooledObject(transform.position, Quaternion.identity);
		}
	}

}