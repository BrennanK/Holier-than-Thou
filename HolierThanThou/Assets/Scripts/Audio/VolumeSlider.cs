using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
	[SerializeField] private Slider slider;

	private void Start()
	{
		//Load Data
		slider.value = PlayerPrefs.GetFloat(slider.name, 1f);
	}

	public void IncrementVolume(float amount)
	{
		slider.value += amount;
	}

	public void SaveData()
	{
		PlayerPrefs.SetFloat(slider.name, slider.value);
	}
}
