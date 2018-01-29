//
// LocalizedAudioSource.cs
//
//
// Written by Niklas Borglund and Jakob Hillerström
//

namespace SmartLocalization
{
using UnityEngine;
using System.Collections;

public class LocalizedAudioSource : MonoBehaviour 
{
	public string localizedKey = "INSERT_KEY_HERE";
	public AudioClip audioClip;
	private AudioSource audioSource;
	
	void Start () 
	{
		//Subscribe to the change language event
		LanguageManager languageManager = LanguageManager.Instance;
		languageManager.OnChangeLanguage += OnChangeLanguage;
		
		//Get the audio source
		audioSource = GetComponent<AudioSource>();
		
		//Run the method one first time
		OnChangeLanguage();
	}

	void OnDestroy()
	{
		if(LanguageManager.HasInstance)
			LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
	}
	
	void OnChangeLanguage()
	{
		//Initialize all your language specific variables here
		audioClip = LanguageManager.Instance.GetAudioClip(localizedKey);
		
		if(audioSource != null)
		{
			audioSource.clip = audioClip;
		}
	}
}
}//namespace SmartLocalization
