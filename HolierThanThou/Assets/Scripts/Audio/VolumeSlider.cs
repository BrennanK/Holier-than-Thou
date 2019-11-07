using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
	[SerializeField] private Slider slider;

	private void Start()
	{
		//Load Data
		slider.value = PlayerPrefs.GetInt(slider.name, 100);
	}

	public void IncrementVolume(int amount)
	{
		slider.value += amount;
	}

	public void SaveData()
	{
		PlayerPrefs.SetInt(slider.name, (int)slider.value);
	}
}
