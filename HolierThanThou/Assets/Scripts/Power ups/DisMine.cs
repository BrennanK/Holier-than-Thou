using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisMine : PowerUp
{
    private GameObject disMine;
    private float positionOffSet;
    private Vector3 position;
    private Quaternion rotation;

    public DisMine(bool _isEnhancement, bool _hasDuration, float _duration, float _radius, GameObject _disMine, float _posistionOffSet) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {
        disMine = _disMine;
        positionOffSet = _posistionOffSet;
    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        position = new Vector3(origin.position.x, origin.position.y - positionOffSet, origin.position.z);
        rotation = new Quaternion(0, origin.rotation.y, 0, 0);

        origin.GetComponent<Competitor>().DisMine(duration, disMine, position, rotation);

    }
}
