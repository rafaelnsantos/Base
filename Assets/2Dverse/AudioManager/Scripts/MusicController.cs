using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;
	private Button button;

	private void Awake () {
		image = GetComponent<Image>();
		button = GetComponent<Button>();
	}

	private void Start () {
		ChangeSprite(AudioSettings.Instance.MusicOn);
		button.onClick.AddListener(SwitchMusic);
		AudioSettings.Instance.HandleMusicSwitch += ChangeSprite;
	}

	private void ChangeSprite (bool isOn) {
		image.sprite = isOn ? On : Off;
	}

	private void OnDestroy () {
		if (AudioSettings.Instance)
			AudioSettings.Instance.HandleMusicSwitch -= ChangeSprite;
	}

	private void SwitchMusic () {
		AudioSettings.Instance.SwitchMusic();
	}

}