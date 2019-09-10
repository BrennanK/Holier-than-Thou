using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject PauseButton;
    public GameObject Joystick;
    public GameObject JumpButton;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
            PauseButton.SetActive(false);
            Joystick.SetActive(false);
            JumpButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            OptionsScreen.SetActive(false);
            PauseButton.SetActive(true);
            Joystick.SetActive(true);
            JumpButton.SetActive(true);
        }
    }

    public void LoadLevelOnClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }



}
