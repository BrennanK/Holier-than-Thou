using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<Competitor> players = new List<Competitor>();

    public Text scoreText;
    public Text endGameScore;
    public Text winnerText;
    

    private void Start()
    {
        foreach (Competitor player in GameObject.FindObjectsOfType<Competitor>())
        {
            players.Add(player);           
        }

        UpdateScoreBoard();
    }

    public void UpdateScore(string name, int point)
    {
        var _competitior = players.Find(x => x.Name == name);


        _competitior.Score += point;

        UpdateScoreBoard();
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

    public void UpdateEndGameUI()
    {
        players = players.OrderByDescending(x => x.Score).ToList();

        winnerText.text = players[0].Name + " Has Won!";

        endGameScore.text = "1st - " + players[0].Name + " - " + players[0].Score + " Points" +
            "\n2nd - " + players[1].Name + " - " + players[1].Score + " Points" +
            "\n3rd - " + players[2].Name + " - " + players[2].Score + " Points" +
            "\n4th - " + players[3].Name + " - " + players[3].Score + " Points" +
            "\n5th - " + players[4].Name + " - " + players[4].Score + " Points" +
            "\n6th - " + players[5].Name + " - " + players[5].Score + " Points" +
            "\n7th - " + players[6].Name + " - " + players[6].Score + " Points" +
            "\n8th - " + players[7].Name + " - " + players[7].Score + " Points";
    }
}
