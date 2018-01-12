using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;

	private void Awake () {
		image = GetComponent<Image>();
	}

	private void Start () {
		ChangeSprite();
		GetComponent<Button>().onClick.AddListener(SwitchEffect);
		AudioSettings.Instance.HandleEffectSwitch += ChangeSprite;
	}

	private void ChangeSprite () {
		image.sprite = AudioSettings.Instance.EffectOn ? On : Off;
	}

	private void OnDestroy () {
		if (AudioSettings.Instance)
			AudioSettings.Instance.HandleEffectSwitch -= ChangeSprite;
	}

	private void SwitchEffect () {
		AudioSettings.Instance.SwitchEffect();
	}

}