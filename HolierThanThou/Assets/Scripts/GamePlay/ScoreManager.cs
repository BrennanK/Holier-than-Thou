using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static int scoreMultiplier = 10;
	public List<Competitor> players = new List<Competitor>();

	public Text scoreText;
	public Text endGameScore;
    public Text lastBallRollingEndGameText;
    public Text lBRWinnerText;
	public Text winnerText;
    public int gameWon;


	private void Start()
	{
		foreach (Competitor player in GameObject.FindObjectsOfType<Competitor>())
		{
			players.Add(player);
		}
        gameWon = 0;

		UpdateScoreBoard();
	}

	public void UpdateScore(string name, int point)
	{
		Competitor _competitior = players.Find(x => x.Name == name);

		_competitior.Score += point;

		UpdateScoreBoard();
	}

	public bool IsPlayerFirst()
	{
		return (players[0].Name == GameObject.FindGameObjectWithTag("Player").GetComponent<Competitor>().Name);
	}

	private void UpdateScoreBoard()
	{
		players = players.OrderByDescending(x => x.Score).ToList();

		scoreText.text = "1st - " + players[0].Name + " - " + players[0].Score + " Points" +
			"\n2nd - " + players[1].Name + " - " + players[1].Score + " Points" +
			"\n3rd - " + players[2].Name + " - " + players[2].Score + " Points" +
			"\n4th - " + players[3].Name + " - " + players[3].Score + " Points" +
			"\n5th - " + players[4].Name + " - " + players[4].Score + " Points" +
			"\n6th - " + players[5].Name + " - " + players[5].Score + " Points" +
			"\n7th - " + players[6].Name + " - " + players[6].Score + " Points" +
			"\n8th - " + players[7].Name + " - " + players[7].Score + " Points";
	}

	public void UpdateEndGameUI(int winnings)
	{
		players = players.OrderByDescending(x => x.Score).ToList();

        if(players[0].name == "Player")
        {
            gameWon = 1;
        }
        

		winnerText.text = players[0].Name + " Has Won!";

		endGameScore.text = "1st - " + players[0].Name + " - " + players[0].Score + " Points" +
			"\n2nd - " + players[1].Name + " - " + players[1].Score + " Points" +
			"\n3rd - " + players[2].Name + " - " + players[2].Score + " Points" +
			"\n4th - " + players[3].Name + " - " + players[3].Score + " Points" +
			"\n5th - " + players[4].Name + " - " + players[4].Score + " Points" +
			"\n6th - " + players[5].Name + " - " + players[5].Score + " Points" +
			"\n7th - " + players[6].Name + " - " + players[6].Score + " Points" +
			"\n8th - " + players[7].Name + " - " + players[7].Score + " Points";
            Debug.Log($"Player won games won is +{gameWon}");
	}

    public void UpdateLastBallRollingEndGameUI(int winnings)
    {
        players = players.OrderByDescending(x => x.Score).ToList();

        if (players[0].name == "Player")
        {
            gameWon = 1;
        }


        lBRWinnerText.text = players[0].Name + " Has Won!";

        lastBallRollingEndGameText.text = "1st - " + players[0].Name + " - " + players[0].Score + " Points" +
            "\n2nd - " + players[1].Name + " - " + players[1].Score + " Points" +
            "\n3rd - " + players[2].Name + " - " + players[2].Score + " Points" +
            "\n4th - " + players[3].Name + " - " + players[3].Score + " Points" +
            "\n5th - " + players[4].Name + " - " + players[4].Score + " Points" +
            "\n6th - " + players[5].Name + " - " + players[5].Score + " Points" +
            "\n7th - " + players[6].Name + " - " + players[6].Score + " Points" +
            "\n8th - " + players[7].Name + " - " + players[7].Score + " Points";
        Debug.Log($"Player won games won is +{gameWon}");
    }
}
