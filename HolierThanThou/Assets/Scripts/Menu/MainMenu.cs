using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Text playerNameText;
	public void Awake()
	{
		ResetNameText();
	}

	public void ResetNameText()
	{
		playerNameText.text = PlayerPrefs.GetString("PLAYER_INPUT_NAME", "");
	}
	public void QuitOnClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif

		Application.Quit();
	}


}
