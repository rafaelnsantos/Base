using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour {

	private AudioSource musicSource, effectSource;

	public bool MusicOn { get; private set; }
	public bool EffectOn { get; private set; }
	public static AudioManager Instance { get; private set; }

	public float MusicVolume {
		get { return musicSource.volume; }
		set { musicSource.volume = value; }
	}

	public float EffectVolume {
		get { return effectSource.volume; }
		set { effectSource.volume = value; }
	}

	public Action<bool> OnMusicSwitch;

	public Action<bool> OnEffectSwitch;

	private void Awake () {
		// Singleton Pattern
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		musicSource = GetComponents<AudioSource>()[0];
		effectSource = GetComponents<AudioSource>()[1];
	}
	
	private void Start () {
		LoadSettings();
	}

	/// <summary>
	/// Play an effect.
	/// </summary>
	/// <param name="clip">Clip to play.</param>
	/// <param name="duration">Duration, default 1.</param>
	/// <param name="pitch">Pitch, default 1.</param>
	public static void PlayEffect (AudioClip clip, float duration = 1f, float pitch = 1f) {
		if (!Instance.EffectOn) return;

		Instance.effectSource.pitch = pitch;
		Instance.effectSource.PlayOneShot(clip, duration);
	}

	/// <summary>
	/// Play a random effect.
	/// </summary>
	/// <param name="clips">Array of Clips to play.</param>
	/// <param name="duration">Duration, default 1.</param>
	/// <param name="lowPitchRange">Low Pitch, defalt 0.95</param>
	/// <param name="highPitchRange">High Pitch, defaltt 1.05</param>
	public static void PlayRandomEffect (AudioClip[] clips, float duration = 1f, float lowPitchRange = 0.95f,
		float highPitchRange = 1.05f) {
		int random = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		PlayEffect(clips[random], duration, randomPitch);
	}

	/// <summary>
	/// Play a music.
	/// </summary>
	/// <param name="clip">Music to play.</param>
	public static void PlayMusic (AudioClip clip) {
		Instance.musicSource.clip = clip;
		if (!Instance.MusicOn) return;

		Instance.musicSource.Play();
	}

	public static void StopMusic () {
		Instance.musicSource.Stop();
	}

	/// <summary>
	/// Switch music on or off. 
	/// </summary>
	/// <returns>Returns actual state.</returns>
	public static void SwitchMusic () {
		Instance.MusicOn = !Instance.MusicOn;

		if (!Instance.MusicOn)
			Instance.musicSource.Stop();
		else
			Instance.musicSource.Play();

		if (Instance.OnMusicSwitch != null) Instance.OnMusicSwitch(Instance.MusicOn);
	}

	/// <summary>
	/// Switch effects on or off.
	/// </summary>
	/// <returns>Returns actual state.</returns>
	public static void SwitchEffect () {
		Instance.EffectOn = !Instance.EffectOn;
		if (Instance.OnEffectSwitch != null) Instance.OnEffectSwitch(Instance.EffectOn);
	}

	private void LoadSettings () {
		MusicOn = SaveManager.GetBool("MusicOn", true);
		EffectOn = SaveManager.GetBool("EffectOn", true);
		musicSource.volume = SaveManager.GetFloat("MusicVolume", 1);
		effectSource.volume = SaveManager.GetFloat("EffectVolume", 1);
	}

	private void SaveSettings () {
		SaveManager.SetBool("MusicOn", MusicOn);
		SaveManager.SetBool("EffectOn", EffectOn);
		SaveManager.SetFloat("MusicVolume", musicSource.volume);
		SaveManager.SetFloat("EffectVolume", effectSource.volume);
	}
	
	private void OnApplicationQuit () {
		SaveSettings();
		GameState.Save();
	}

	private void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) SaveSettings();
	}

}