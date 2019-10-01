using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantTouchThis : PowerUp
{
    public CantTouchThis(bool _hasDuration, float _duration, float _radius) : base(_hasDuration, _duration, _radius)
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

        _competitior.GetComponent<Competitor>().CantTouchMe(duration);



        Debug.Log("Can't Touch This! Power Up Used!");

    }

    
}
