using UnityEngine;
using System.Collections.Generic;
using System.Security.Policy;

public class ObjectPooler : MonoBehaviour {

	public GameObject objectToPool;
	public int amountToPool;
	public bool shouldExpand;
	private List<GameObject> pooledObjects;

	private void Start () {
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < amountToPool; i++) {
			GameObject obj = (GameObject) Instantiate(objectToPool);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject (Vector3 position, Quaternion rotation) {
		for (int i = 0; i < amountToPool; i++) {
			if (pooledObjects[i].activeInHierarchy) continue;

			pooledObjects[i].transform.position = position;
			pooledObjects[i].transform.rotation = rotation;
			pooledObjects[i].SetActive(true);
			return pooledObjects[i];
		}

		if (!shouldExpand) return null;

		GameObject obj = (GameObject) Instantiate(objectToPool);
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive(true);
		pooledObjects.Add(obj);
		amountToPool++;
		return obj;
	}

}