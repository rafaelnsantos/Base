using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	private bool active;
	public float Time;
	public GameObject Object;

	private void Start () {
		GetComponent<Button>().onClick.AddListener(Switch);
		Object.transform.DOScale(Vector2.zero, 0);
	}

	private void Switch () {
		active = !active;
		Object.transform.DOScale(Vector2.one * (active ? 1 : 0), Time);
	}

	public void Hide () {
		if (active) Switch();
	}

}