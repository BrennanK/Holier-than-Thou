using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	public string SFXSlider = "SoundsSliderBar";
	public string MusicSlider = "MusicSliderBar";

	[Range(0.5f, 1.0f)]
	public float rangeMin;
	[Range(1.0f, 1.5f)]
	public float rangeMax;

	[SerializeField] private int maxVolume = 100;


	// Start is called before the first frame update
	void Awake()
    {
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.spatialBlend = s.spatialBlend;
		}
    }

	public void Play(string name, bool pitchModulate = true)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s != null)
		{
			float sliderVolume = 0;

			switch (s.type)
			{
				case Sound.SoundType.SFX:
					s.source.pitch = UnityEngine.Random.Range(rangeMin, rangeMax);
					sliderVolume = (float)PlayerPrefs.GetFloat(SFXSlider)/ (float)maxVolume;
					break;
				case Sound.SoundType.Music:
					sliderVolume = (float)PlayerPrefs.GetFloat(MusicSlider)/ (float)maxVolume;
					break;
				default:
					sliderVolume = 0;
					break;
			}

			s.source.volume = s.volume * sliderVolume;
			s.source.Play();
		}
	}
}
