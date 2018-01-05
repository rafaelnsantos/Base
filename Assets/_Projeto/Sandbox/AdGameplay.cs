using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdGameplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AdMobiManager.Instance.RequestBanner();	
	}
	
}
