using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BallsOfSteel : PowerUp
{
    public BallsOfSteel(bool _isEnhancement, bool _hasDuration, float _duration, float _radius) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {

    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        List<Collider> enemies = Physics.OverlapSphere(origin.position, radius).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].GetComponent<Competitor>() || enemies[i].transform == origin)
            {
                enemies.Remove(enemies[i]);
                i--;
            }
        }
        if (enemies.Count == 0)
        {
            return;
        }
        else
        {
            foreach (Collider enemy in enemies)
            {
                var bounce = origin.GetComponent<BounceFunction>();
                var rb = enemy.GetComponent<Rigidbody>();
                var competitor = origin.GetComponent<Competitor>();

                competitor.BallOfSteel(duration, bounce);

            }
        }

        Debug.Log("Balls of Steel Power Up Used by " + name);

    }

}

