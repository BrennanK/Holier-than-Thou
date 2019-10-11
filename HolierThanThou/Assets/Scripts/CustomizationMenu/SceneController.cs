using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	List<int> scenes = new List<int>();

	// Start is called before the first frame update
	void Start()
	{
		LoadScenes();
		//When you switch to a scene, add the current scene's buildindex to that list.
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
				saveData += ',';
			}
		}
		PlayerPrefs.SetString("Scenes", saveData);
	}

	public void GoToPreviousScene()
	{
		//When you're going back a scene, remove the current last element of the scenes list.
		if (scenes.Count > 0)
		{
			scenes.RemoveAt(scenes.Count - 1);
		}
		if (scenes.Count >= 2)
		{
			//scenes.Count - 1 is the index for the current scene.
			//scenes.Count will always at least be 1, because it is incremented in the Start() method.
			SceneManager.LoadScene(scenes[scenes.Count - 2]);
		}
		else
		{
			SceneManager.LoadScene(0);
		}
	}
}
