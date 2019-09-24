using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static bool gameRunning;

    public Text startText;
    private float startTimer = 5f;

    public GameObject EndMatchScreen;
    public GameObject GameUI;

    public Text inGameTimer;

    private ScoreManager scoreManager;

    public float matchTimer;

    private void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();
        EndMatchScreen.SetActive(false);
        gameRunning = false;
        print("Ready!");
        StartCoroutine(StartGame());
        inGameTimer.text = "Time " + matchTimer;
    }

    private void Update()
    {
        if(matchTimer > 0 && gameRunning)
        {
            matchTimer -= Time.deltaTime;
            inGameTimer.text = "Time " + Mathf.Round(matchTimer);
                
        }
        if(matchTimer <= 0)
        {
            EndMatch(); 

        }        

    }

    IEnumerator StartGame()
    {
        startText.text = "Ready";
        yield return new WaitForSeconds(1f);
        startText.text = "3";
        yield return new WaitForSeconds(1f);
        startText.text = "2";
        yield return new WaitForSeconds(1f);
        startText.text = "1";
        yield return new WaitForSeconds(1f);
        startText.text = "GO!";
        gameRunning = true;
        yield return new WaitForSeconds(1f);
        Destroy(startText);
    }

    void EndMatch()
    {
        gameRunning = false;
        EndMatchScreen.SetActive(true);
        GameUI.SetActive(false);
        scoreManager.UpdateEndGameUI();
    }


}
