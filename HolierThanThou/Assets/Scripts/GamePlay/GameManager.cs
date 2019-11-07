using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject EndMatchScreen;
    [SerializeField] private GameObject lBREndMatchScreen;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private Text startText;
	[SerializeField] private Text inGameTimer;
	[SerializeField] public float matchTimer = 120.0f; // 2 minute rounds
    [SerializeField] private GameObject[] CrownSpawnPoints;
    [SerializeField] private float crownSpawnTimer = 9f;

    private PlayerAchievementTracker playerAchievements;

    public static bool gameRunning { get; private set; } = false;
    private float startTimer = 5f;
	private ScoreManager scoreManager;
	private GameObject playerCustomizer;
	private bool playerWon;
	private bool gameOver = false;
	private List<GameObject> spawnPoints;
	private int numCrowns = 3;


	private void Start()
    {
		InitializeTextField(ref startText, "StartText");
		InitializeTextField(ref inGameTimer, "Timer");
		InitializeGameObject(ref EndMatchScreen, "EndMatchScreen");
		InitializeGameObject(ref GameUI, "GameUI");
		InitializeCrownSpawnPoints();

		scoreManager = gameObject.GetComponent<ScoreManager>();
        EndMatchScreen.SetActive(false);
        StartCoroutine(StartGame());
		inGameTimer.text = "Time " + matchTimer;
		playerCustomizer = GameObject.FindGameObjectWithTag("Player");
        playerAchievements = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAchievementTracker>();
	}

	private void InitializeCrownSpawnPoints()
	{
		if(CrownSpawnPoints.Length != numCrowns)
		{
			Debug.LogError($"Crown Spawn Points array does not have {numCrowns} items in it.\nDrag and Drop the Crown SpawnPoints into the GameManager Script.");
		}
	}

	private void InitializeTextField(ref Text component, string textName)
	{
		if (component == null)
		{
			Text[] Texts = Resources.FindObjectsOfTypeAll<Text>();
			foreach (Text item in Texts)
			{
				if(item.name == textName)
				{
					component = item;
				}
			}
		}
	}

	private void InitializeGameObject(ref GameObject component, string objectName)
	{
		if (component == null)
		{
			GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
			foreach (GameObject item in gameObjects)
			{
				if (item.name == objectName)
				{
					component = item;
				}
			}
		}
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
		StartCoroutine(SpawnCrowns());
		yield return new WaitForSeconds(1f);
        startText.gameObject.SetActive(false);
    }

	IEnumerator SpawnCrowns()
	{
		if (gameRunning)
		{
			yield return new WaitForSeconds(crownSpawnTimer);
			//list out all spawnPoints that do NOT have an active crown
			spawnPoints = new List<GameObject>();
			foreach (GameObject crownSpawn in CrownSpawnPoints)
			{
				if (!crownSpawn.activeSelf)
				{
					spawnPoints.Add(crownSpawn);
				}
			}
			if (spawnPoints.Count > 0)
			{
				//randomly choose between the available spawnPoints to spawn a crown in.
				int randPoint = Random.Range(0, spawnPoints.Count);
				//Spawn the crown by setting it to active.
				spawnPoints[randPoint].SetActive(true);
			}
			//repeat
			StartCoroutine(SpawnCrowns());
		}
		yield return new WaitForSeconds(0);
	}

    void EndMatch()
    {
        if (FindObjectOfType<LastBallRolling>())
        {
            lBREndMatchScreen.SetActive(true);
            scoreManager.UpdateLastBallRollingEndGameUI(PayWinner());
        }
        else
        {
            EndMatchScreen.SetActive(true);
            scoreManager.UpdateEndGameUI(PayWinner());
        }
        gameRunning = false;
        GameUI.SetActive(false);
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAchievementTracker>();

        PlayerProfile playerProfileForThisMatch = new PlayerProfile(1, scoreManager.gameWon, playerAchievements.hitSomebody, playerAchievements.knockedFirst, playerAchievements.ScoredAfterHit, scoreManager.playerLast, playerAchievements.scoredGoal, scoreManager.placedFourth, playerAchievements.usedPowerUp, playerAchievements.usedJump);
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
