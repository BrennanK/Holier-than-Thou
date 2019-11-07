using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounce : PowerUp
{
    private float bounceMultiplier;

    public SuperBounce(bool _isEnhancement, bool _hasDuration, float _duration, float _radius, float _bounceMultipler) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {
        bounceMultiplier = _bounceMultipler;
    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        var bounce = origin.GetComponent<Bounce>();
        var competitor = origin.GetComponent<Competitor>();

        bounce.bouceOffForce *= bounceMultiplier;

        competitor.NormalBounce(origin, duration, bounceMultiplier);

    }

    
}
