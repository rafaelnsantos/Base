using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public Slider effectSlider, musicSlider;
	public Text effectOn, musicOn;

	private void Start () {
		musicSlider.value = AudioSettings.Instance.MusicVolume;
		effectSlider.value = AudioSettings.Instance.EffectVolume;
		
		musicOn.text = AudioSettings.Instance.MusicOn ? "ON" : "OFF";
		effectOn.text = AudioSettings.Instance.EffectOn ? "ON" : "OFF";
	}

	public void SetEffectVolume () {
		AudioSettings.Instance.EffectVolume = effectSlider.value;
	}

	public void SetMusicVolume () {
		AudioSettings.Instance.MusicVolume = musicSlider.value;
	}

	public void SwitchMusic () {
		bool state = AudioSettings.Instance.SwitchMusic();
		musicOn.text = state ? "ON" : "OFF";
	}

	public void SwitchEffect () {
		bool state = AudioSettings.Instance.SwitchEffect();
		effectOn.text = state ? "ON" : "OFF";
	}
}
