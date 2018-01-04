using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {

	public Slider effectSlider, musicSlider;
	public Text effectOn, musicOn;

	private void Start () {
		musicSlider.value = AudioManager.Instance.MusicVolume;
		effectSlider.value = AudioManager.Instance.EffectVolume;
	}

	public void SetEffectVolume () {
		AudioManager.Instance.EffectVolume = effectSlider.value;
	}

	public void SetMusicVolume () {
		AudioManager.Instance.MusicVolume = musicSlider.value;
	}

	public void SwitchMusic () {
		bool state = AudioManager.Instance.SwitchMusic();
		musicOn.text = state ? "ON" : "OFF";
	}

	public void SwitchEffect () {
		bool state = AudioManager.Instance.SwitchEffect();
		effectOn.text = state ? "ON" : "OFF";
	}
}
