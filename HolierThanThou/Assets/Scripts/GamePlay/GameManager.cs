using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static bool gameRunning;
    public Text startText;
    public GameObject EndMatchScreen;
    public GameObject GameUI;
    public Text inGameTimer;
    public float matchTimer;

    private float startTimer = 5f;
    private ScoreManager scoreManager;
	private GameObject playerCustomizer;
	private bool playerWon;
	private bool gameOver = false;

    private void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();
        EndMatchScreen.SetActive(false);
        gameRunning = false;
        StartCoroutine(StartGame());
        inGameTimer.text = "Time " + matchTimer;
		playerCustomizer = GameObject.FindGameObjectWithTag("Player");
	}

    private void Update()
    {
        if(matchTimer > 0 && gameRunning)
        {
            matchTimer -= Time.deltaTime;
            inGameTimer.text = "Time " + Mathf.Round(matchTimer);
            
        }
        if(matchTimer <= 0 && !gameOver)
        {
			gameOver = true;
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
		scoreManager.UpdateEndGameUI(PayWinner());

        PlayerProfile playerProfileForThisMatch = new PlayerProfile(1, scoreManager.gameWon);
        SaveGameManager.instance.IncrementSavedData(playerProfileForThisMatch);
        StoreServices.AchievementManager.instance.UpdateAllAchievements(playerProfileForThisMatch);
	}

	private int PayWinner()
	{
		int winnings = (10 - playerCustomizer.GetComponent<Competitor>().Score) * ScoreManager.scoreMultiplier;
		playerCustomizer.GetComponent<PlayerCustomization>().addCurrency(winnings);
		int monies = playerCustomizer.GetComponent<PlayerCustomization>().currency;
		return winnings;
	}
}
