using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;

	private void Awake () {
		image = GetComponent<Image>();
	}

	private void Start () {
		ChangeSprite(AudioManager.Instance.EffectOn);
		AudioManager.Instance.HandleEffectSwitch += ChangeSprite;
	}

	private void ChangeSprite (bool isOn) {
		image.sprite = isOn ? On : Off;
	}

	private void OnDestroy () {
		AudioManager.Instance.HandleEffectSwitch -= ChangeSprite;
	}

	public void SwitchEffect () {
		AudioManager.Instance.SwitchEffect();
	}

}