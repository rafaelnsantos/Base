using UnityEngine;

public class Crate : MonoBehaviour, ITouchable {

	public void OnTouch () {
		gameObject.SetActive(false);
	}

}
