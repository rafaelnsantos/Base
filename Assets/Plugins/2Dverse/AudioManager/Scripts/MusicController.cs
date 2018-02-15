using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;

	private void Awake () {
		image = GetComponent<Image>();
	}

	private void Start () {
		ChangeSprite(AudioManager.Instance.MusicOn);
		GetComponent<Button>().onClick.AddListener(SwitchMusic);
		AudioManager.Instance.HandleMusicSwitch += ChangeSprite;
	}

	private void ChangeSprite (bool isOn) {
		image.sprite = isOn ? On : Off;
	}

	private void OnDestroy () {
		AudioManager.Instance.HandleMusicSwitch -= ChangeSprite;
	}

	private void SwitchMusic () {
		AudioManager.Instance.SwitchMusic();
	}

}