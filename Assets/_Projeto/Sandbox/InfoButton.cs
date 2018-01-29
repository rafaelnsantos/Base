using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour {


	public GameObject InfoPanel;

	private void Start () {
		GetComponent<Button>().onClick.AddListener(Show);
	}

	public void Show () {
		InfoPanel.SetActive(true);
	}

	public void Hide () {
		InfoPanel.gameObject.GetComponentInChildren<Animator>().SetBool("Hide", true);
	}

}
