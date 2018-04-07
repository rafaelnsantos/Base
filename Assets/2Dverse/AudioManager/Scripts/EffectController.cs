using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;
	private Slider slider;

	private void Awake () {
		image = GetComponent<Image>();
		slider = GetComponentInChildren<Slider>();
	}

	private void Start () {
		ChangeSprite(AudioManager.Instance.EffectOn);
		AudioManager.Instance.OnEffectSwitch += ChangeSprite;
		slider.onValueChanged.AddListener(ChangeVolume);
		slider.value = AudioManager.Instance.EffectVolume;
	}

	private void ChangeSprite (bool isOn) {
		image.sprite = isOn ? On : Off;
		slider.interactable = isOn;
	}

	private void OnDestroy () {
		AudioManager.Instance.OnEffectSwitch -= ChangeSprite;
	}

	public void SwitchEffect () {
		AudioManager.SwitchEffect();
	}
	
	private void ChangeVolume (float value) {
		AudioManager.Instance.EffectVolume = value;
	}

}