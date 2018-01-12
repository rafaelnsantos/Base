using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

	public Sprite On, Off;
	private Image image;

	private void Awake () {
		image = GetComponent<Image>();
	}

	private void Start () {
		ChangeSprite();
		GetComponent<Button>().onClick.AddListener(SwitchMusic);
		AudioSettings.Instance.HandleMusicSwitch += ChangeSprite;
	}

	private void ChangeSprite () {
		image.sprite = AudioSettings.Instance.MusicOn ? On : Off;
	}

	private void OnDestroy () {
		if (AudioSettings.Instance)
			AudioSettings.Instance.HandleMusicSwitch -= ChangeSprite;
	}

	private void SwitchMusic () {
		AudioSettings.Instance.SwitchMusic();
	}

}