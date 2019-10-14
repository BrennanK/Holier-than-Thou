using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private List<int> scenes = new List<int>();

	// Start is called before the first frame update
	void Start()
	{
		LoadScenes();
		//When you switch to a scene, add the current scene's buildIndex to that list.
		scenes.Add(SceneManager.GetActiveScene().buildIndex);
	}

	void LoadScenes()
	{
		string toParse = PlayerPrefs.GetString("Scenes");
		char[] commas = { ',' };
		string[] sceneIndices = toParse.Split(commas, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < sceneIndices.Length; i++)
		{
			scenes.Add(System.Convert.ToInt32(sceneIndices[i]));
		}
		int debug = 0;
		debug++;
	}

	public void OnDestroy()
	{
		SaveScenes();
	}

	void SaveScenes()
	{
		string saveData = "";
		for (int i = 0; i < scenes.Count; i++)
		{
			saveData += scenes[i];
			//Don't put a comma on the end of the string.
			if (i < scenes.Count - 1)
			{
				//Separate entries by comma
				saveData += ',';
			}
		}
		PlayerPrefs.SetString("Scenes", saveData);
	}

	public void OnApplicationQuit()
	{
		scenes.Clear();
		SaveScenes();
	}

	public void GoToPreviousScene()
	{
		//When you're going back a scene, remove the current last element of the scenes list.
		if (scenes.Count > 0)
		{
			scenes.RemoveAt(scenes.Count - 1);
		}
		//If there are at least two scenes, you can go back one scene.
		if (scenes.Count >= 2)
		{
			//scenes.Count - 1 is the index for the current scene.
			//scenes.Count will always at least be 1, because it is incremented in the Start() method.
			SceneManager.LoadScene(scenes[scenes.Count - 2]);
		}
		// if there is only one scene, it SHOULD be the main scene, but this will load the main menu in any other case.
		else
		{
			SceneManager.LoadScene(0);
		}
	}

	public void GoToScene(int index)
	{
		SceneManager.LoadScene(index);
	}

	public void GoToNextScene()
	{
		//this_scene++;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
