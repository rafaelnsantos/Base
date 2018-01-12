using UnityEngine;
using Random = UnityEngine.Random;

public class AudioSettings : Savable {

	private AudioSource musicSource, effectSource;

	public bool MusicOn { get; private set; }
	public bool EffectOn { get; private set; }
	public static AudioSettings Instance { get; private set; }

	public float MusicVolume {
		get { return musicSource.volume; }
		set { musicSource.volume = value; }
	}

	public float EffectVolume {
		get { return effectSource.volume; }
		set { effectSource.volume = value; }
	}

	public delegate void OnMusicSwitch ();
	public event OnMusicSwitch HandleMusicSwitch;

	public delegate void OnEffectSwitch ();
	public event OnEffectSwitch HandleEffectSwitch;

	private void Awake () {
		// Singleton Pattern
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		musicSource = gameObject.GetComponents<AudioSource>()[0];
		effectSource = gameObject.GetComponents<AudioSource>()[1];
		Load();
	}

	private void Start () {
		PlayMusic(musicSource.clip);
	}

	/// <summary>
	/// Play an effect.
	/// </summary>
	/// <param name="clip">Clip to play.</param>
	/// <param name="duration">Duration, default 1.</param>
	/// <param name="pitch">Pitch, default 1.</param>
	public void PlayEffect (AudioClip clip, float duration = 1f, float pitch = 1f) {
		if (!EffectOn) return;

		effectSource.pitch = pitch;
		effectSource.PlayOneShot(clip, duration);
	}

	/// <summary>
	/// Play a random effect.
	/// </summary>
	/// <param name="clips">Array of Clips to play.</param>
	/// <param name="duration">Duration, default 1.</param>
	/// <param name="lowPitchRange">Low Pitch, defalt 0.95</param>
	/// <param name="highPitchRange">High Pitch, defaltt 1.05</param>
	public void PlayRandomEffect (AudioClip[] clips, float duration = 1f, float lowPitchRange = 0.95f,
		float highPitchRange = 1.05f) {
		int random = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		PlayEffect(clips[random], duration, randomPitch);
	}

	/// <summary>
	/// Play a music.
	/// </summary>
	/// <param name="clip">Music to play.</param>
	public void PlayMusic (AudioClip clip) {
		musicSource.clip = clip;
		if (!MusicOn) return;

		musicSource.Play();
	}

	public void StopMusic () {
		musicSource.Stop();
	}

	/// <summary>
	/// Switch music on or off. 
	/// </summary>
	/// <returns>Returns actual state.</returns>
	public void SwitchMusic () {
		MusicOn = !MusicOn;

		if (!MusicOn)
			musicSource.Stop();
		else
			musicSource.Play();

		HandleMusicSwitch?.Invoke();
	}

	/// <summary>
	/// Switch effects on or off.
	/// </summary>
	/// <returns>Returns actual state.</returns>
	public void SwitchEffect () {
		EffectOn = !EffectOn;
		HandleEffectSwitch?.Invoke();
	}

	protected override void Load () {
		MusicOn = SaveManager.GetBool("MusicOn", true);
		EffectOn = SaveManager.GetBool("EffectOn", true);
		musicSource.volume = SaveManager.GetFloat("MusicVolume", 1);
		effectSource.volume = SaveManager.GetFloat("EffectVolume", 1);
	}

	protected override void Save () {
		SaveManager.SetBool("MusicOn", MusicOn);
		SaveManager.SetBool("EffectOn", EffectOn);
		SaveManager.SetFloat("MusicVolume", musicSource.volume);
		SaveManager.SetFloat("EffectVolume", effectSource.volume);
	}

}