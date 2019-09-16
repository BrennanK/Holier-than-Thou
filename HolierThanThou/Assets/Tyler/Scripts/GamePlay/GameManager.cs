using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text startText;
    private float startTimer = 5f;
    private bool gameStarted;

    public GameObject EndMatchScreen;
    public GameObject GameUI;

    public Text inGameTimer;

    private ScoreManager scoreManager;

    public float matchTimer;

    private void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();
        EndMatchScreen.SetActive(false);
        gameStarted = false;
        print("Ready!");
        StartCoroutine(StartGame());
        inGameTimer.text = "Time " + matchTimer;
    }

    private void Update()
    {
        if(matchTimer > 0 && gameStarted)
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
        gameStarted = true;
        yield return new WaitForSeconds(1f);
        Destroy(startText);

        //while(startTimer >= 0)
        //{
        //    print(Mathf.Round(startTimer));
        //    startTimer--;
        //    yield return new WaitForSeconds(1f);
        //}
        //print("Start!");
        //gameStarted = true;
    }

    void EndMatch()
    {
        EndMatchScreen.SetActive(true);
        GameUI.SetActive(false);
        scoreManager.UpdateEndGameUI();
    }


}
