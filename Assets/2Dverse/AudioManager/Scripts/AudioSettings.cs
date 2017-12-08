using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {

	private Slider slider;
	public Text Text;

	private void Awake () {
		slider = GetComponentInChildren<Slider>();
	}
	
	public void SetEffectVolume () {
		AudioManager.Instance.SetEffectVolume(slider.value);
	}

	public void SetMusicVolume () {
		AudioManager.Instance.SetMusivVolume(slider.value);
	}

	public void SwitchMusic () {
		bool state = AudioManager.Instance.SwitchMusic();
		SetText(state);
	}

	public void SwitchEffect () {
		bool state = AudioManager.Instance.SwitchEffect();
		SetText(state);
	}

	private void SetText (bool state) {
		Text.text = state ? "ON" : "OFF";
	}
	
}
