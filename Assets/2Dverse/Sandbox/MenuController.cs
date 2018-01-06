using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	private bool active;
	public float Time;
	private GameObject Object;

	private void Awake () {
		Object = transform.GetChild(0).gameObject;
	}

	private void Start () {
		GetComponent<Button>().onClick.AddListener(Switch);

		Object.transform.DOScale(Vector2.zero, 0);
	}

	public void Switch () {
		active = !active;
		if (active) Object.SetActive(true);
		Object.transform.DOScale(Vector2.one * (active ? 1 : 0), Time).OnComplete(() => {
			if (!active) Object.SetActive(false);
		});
	}

}