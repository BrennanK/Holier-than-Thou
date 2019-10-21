using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoFast : PowerUp
{ 
    private float aiSpeedMultiplier;
    private float playerSpeedMultiplier;
    private float speedMultiplier;

    

    public GottaGoFast(bool _isEnhancement, bool _hasDuration, float _duration, float _radius, float _aiSpeedMultiplier, float _playerSpeedMultiplier) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {
        aiSpeedMultiplier = _aiSpeedMultiplier;
        playerSpeedMultiplier = _playerSpeedMultiplier;
    }


    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        if (origin.GetComponent<RigidBodyControl>())
        {
            speedMultiplier = playerSpeedMultiplier;
            origin.GetComponent<RigidBodyControl>().speed *= playerSpeedMultiplier;
            
        }
        else
        {
            speedMultiplier = aiSpeedMultiplier;
            origin.GetComponent<AIBehavior>().velocity *= aiSpeedMultiplier;
            
        }
        Debug.Log("Gotta go fast! Power Up Used by " + name);

        origin.GetComponent<Competitor>().WentFast(origin, duration, speedMultiplier);

    }

}
