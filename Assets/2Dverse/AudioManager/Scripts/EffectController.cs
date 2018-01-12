using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;
	private Button button;

	private void Awake () {
		image = GetComponent<Image>();
	}

	private void Start () {
		ChangeSprite(AudioSettings.Instance.EffectOn);
		AudioSettings.Instance.HandleEffectSwitch += ChangeSprite;
	}

	private void ChangeSprite (bool isOn) {
		image.sprite = isOn ? On : Off;
	}

	private void OnDestroy () {
		if (AudioSettings.Instance)
			AudioSettings.Instance.HandleEffectSwitch -= ChangeSprite;
	}

	public void SwitchEffect () {
		AudioSettings.Instance.SwitchEffect();
	}

}