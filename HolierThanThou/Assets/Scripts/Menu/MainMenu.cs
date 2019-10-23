using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private int levelBuildIndexStart;
	[SerializeField] private int levelBuildIndexEnd;
	[SerializeField] private int SPECIFIC_LEVEL = 2;
	[SerializeField] private bool randomize = true;
	public void QuitOnClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif

		Application.Quit();
	}

	public void LoadLevel()
	{
		if (randomize)
		{
			int randInt = Random.Range(levelBuildIndexStart, levelBuildIndexEnd + 1);
			Debug.Log($"Scene to be loaded: {randInt}");
			SceneManager.LoadScene(randInt);
		}
		else
		{
			SceneManager.LoadScene(SPECIFIC_LEVEL);
		}
	}

}
