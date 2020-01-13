using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject GameUI;
    public GameObject CrownUI;

    private bool isPaused;
    AudioManager am;

    private void Start()
	{
		if(this.gameObject.name != "MenuKit")
		{
			Debug.LogError($"You are using an outdated version of the menu UI. \n " +
				$"Get the new menu UI in Resources / Prefabs / Menus, called MenuKit. ");
		}
		isPaused = false;
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        CrownUI = GameObject.Find("DebugCanvas");
    }

    private void Update()
    {
		if(am == null)
		{
			am = FindObjectOfType<AudioManager>();
		}
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
            GameUI.SetActive(false);

            CrownUI.transform.GetChild(0).GetComponent<Text>().enabled = false;
            CrownUI.transform.GetChild(1).GetComponent<Text>().enabled = false;
            CrownUI.transform.GetChild(2).gameObject.SetActive(false);
            //CrownUI.transform.GetChild(2).GetComponent<Image>().enabled = false;
            //CrownUI.transform.GetChild(2).GetComponentInChildren<Text>().enabled = false;
                
            
            
        }
        else
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            OptionsScreen.SetActive(false);
            GameUI.SetActive(true);

            CrownUI.transform.GetChild(0).GetComponent<Text>().enabled = true;
            CrownUI.transform.GetChild(2).gameObject.SetActive(true);
            //CrownUI.transform.GetChild(2).GetComponent<Image>().enabled = true;
            //CrownUI.transform.GetChild(2).GetComponentInChildren<Text>().enabled = true;

        }
    }

    public void LoadLevelOnClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Rematch()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ButtonClick()
    {
        if (am != null)
        {
            am.Play("ButtonClick");
        }
    }
}
