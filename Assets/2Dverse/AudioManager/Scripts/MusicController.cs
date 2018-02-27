using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;
	private Slider slider;

	private void Awake () {
		image = GetComponent<Image>();
		slider = GetComponentInChildren<Slider>();
	}

	private void Start () {
		ChangeSprite(AudioManager.Instance.MusicOn);
		GetComponent<Button>().onClick.AddListener(SwitchMusic);
		AudioManager.Instance.HandleMusicSwitch += ChangeSprite;
		slider.onValueChanged.AddListener(ChangeVolume);
		slider.value = AudioManager.Instance.MusicVolume;
	}
	
	private void ChangeSprite (bool isOn) {
		image.sprite = isOn ? On : Off;
		slider.interactable = isOn;
	}

	private void OnDestroy () {
		AudioManager.Instance.HandleMusicSwitch -= ChangeSprite;
	}

	private void SwitchMusic () {
		AudioManager.SwitchMusic();
	}

	private void ChangeVolume (float value) {
		AudioManager.Instance.MusicVolume = value;
	}

}