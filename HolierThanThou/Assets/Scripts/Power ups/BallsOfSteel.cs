using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BallsOfSteel : PowerUp
{
    private Material ballOfSteelMaterial;


    public BallsOfSteel(bool _isEnhancement, bool _hasDuration, float _duration, float _radius, Material _material) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {
        ballOfSteelMaterial = _material;
    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        //var bounce = origin.GetComponent<BounceFunction>();
        var bounce = origin.GetComponent<Bounce>();

        Competitor competitor = origin.GetComponent<Competitor>();

        bounce.enabled = false;

        if (origin.childCount > 1)
        {
            origin = origin.GetChild(1);
            if (origin.childCount > 0)
            {
                origin = origin.GetChild(0);
                origin.GetComponent<MeshRenderer>().material = ballOfSteelMaterial;
            }
        }
        
        competitor.BallOfSteel(origin, duration);

        Debug.Log("Balls of Steel Power Up Used by " + name);

    }

}

