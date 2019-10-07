using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakySnake : PowerUp
{
    public SneakySnake(bool _isEnhancement, bool _hasDuration, float _duration, float _radius) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {

    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);
        List<Competitor> players = new List<Competitor>();
        foreach (Competitor player in GameObject.FindObjectsOfType<Competitor>())
        {
            players.Add(player);
        }

        var _competitior = players.Find(x => x.Name == name);

        _competitior.GetComponent<Competitor>().CantFindMe(duration);

        Debug.Log("Sneaky Snake Power Up Used by " + name);

    }

    
}
