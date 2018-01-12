using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Touch : Pausable {

	public bool UsingPool = true;

	private CrateSpawnerNoPool c;
	
	private void Awake () {
		if (!UsingPool) {
			c = GetComponent<CrateSpawnerNoPool>();
		}
	}

	protected override void PausableUpdate () {
		if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			Mover();
		}
	}

	void Mover() {
		Vector2 touchPosition;

#if UNITY_EDITOR
		touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); 
		Check(touchPosition);
#else
		for (int i = 0; i < Input.touchCount; i++) {
			touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position); 
			Check(touchPosition);				
		}
#endif	
	}

	//para verificar se o toque foi em algum caixa
	void Check (Vector2 touchPosition) {
		RaycastHit2D hitInformation = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 0);
		if (hitInformation.collider == null || !hitInformation.collider.tag.Equals("Box")) return;

		if (UsingPool)
			hitInformation.collider.gameObject.SetActive(false);
		else {
			Destroy(hitInformation.collider.gameObject);
			c.Spawned--;
		}
	}
}
