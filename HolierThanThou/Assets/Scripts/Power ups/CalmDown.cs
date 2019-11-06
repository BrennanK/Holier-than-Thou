using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CalmDown : PowerUp
{
    private float aiSpeedMultiplier;
    private float playerSpeedMultiplier;
    private float speedMultiplier;

    public CalmDown(bool _isEnhancement, bool _hasDuration, float _duration, float _radius, float _aiSpeedMultiplier, float _playerSpeedMultiplier) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {
        aiSpeedMultiplier = _aiSpeedMultiplier;
        playerSpeedMultiplier = _playerSpeedMultiplier;
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
                var competitor = enemy.GetComponent<Competitor>();

                if (!competitor.untouchable)
                {
                    if (competitor.GetComponent<RigidBodyControl>())
                    {
                        speedMultiplier = playerSpeedMultiplier;
                        competitor.GetComponent<RigidBodyControl>().speed *= playerSpeedMultiplier;
                    }

                    else
                    {
                        speedMultiplier = aiSpeedMultiplier;
                        competitor.GetComponent<AIStateMachine>().Velocity *= aiSpeedMultiplier;
                    }

                    competitor.GetComponent<Competitor>().BeenSlowed(competitor, duration, speedMultiplier);
                }
            }   
        }
        Debug.Log("Calm Down Power Up Used by " + name);
    }


}
