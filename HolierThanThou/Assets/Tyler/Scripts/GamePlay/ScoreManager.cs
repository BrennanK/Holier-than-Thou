using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<Competitor> players = new List<Competitor>();
    

    private void Start()
    {
        foreach (Competitor player in GameObject.FindObjectsOfType<Competitor>())
        {
            players.Add(player);           
        }
    }

    public void UpdateScore(string name, int point)
    {
        var _competitior = players.Find(x => x.Name == name);
        Debug.Log(_competitior.name + " found in the list");

        _competitior.Score += point;

        Debug.Log(_competitior.name + "'s Score: " + _competitior.Score);
    }

}
