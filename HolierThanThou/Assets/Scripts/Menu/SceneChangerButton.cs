using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerButton : MonoBehaviour
{
	private SceneController sceneController;
	private void Start()
	{
		sceneController = FindObjectOfType<SceneController>();
	}
	public void GoToPreviousScene()
	{
        SceneManager.LoadScene(0);

        /*
		if(sceneController != null)
		{
			sceneController.GoToPreviousScene();
		} 
		else
		{
			SceneManager.LoadScene(0);
		}
        */
	}
}
