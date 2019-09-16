using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpValues", menuName = "Power Ups", order = 1)]
public class PowerUpEditor : ScriptableObject
{
    [HideInInspector]
    public bool BZ_hasDuration = false;
    [HideInInspector]
    public float BZ_duration = 0f;
    [Header("Blast Zone - 0")]
    public float BZ_radius;
    public float BZ_power;
    public float BZ_upwardForce;

    [HideInInspector]
    public bool CO_hasDuration = true;
    [Header("Chillout - 1")]
    public float CO_duration;
    public float CO_radius;

    [HideInInspector]
    public bool GF_hasDuration = true;
    [HideInInspector]
    public float GF_radius = 0f;
    [Header("Gotta Go Fast - 2")]
    public float GF_duration;
    public float GF_speedMultiplier;

    [HideInInspector]
    public bool CTT_hasDuration = true;
    [HideInInspector]
    public float CTT_radius = 0f;
    [Header("Cant Touch This - 3")]
    public float CTT_duration;

    [HideInInspector]
    public bool SS_hasDuration = true;
    [HideInInspector]
    public float SS_radius = 0f;
    [Header("Sneaky Snake - 4")]
    public float SS_duration;

    [HideInInspector]
    public bool TH_hasDuration = true;
    [HideInInspector]
    public float TH_radius = 0f;
    [Header("Thiccness - 5")]
    public float TH_duration;

    [HideInInspector]
    public bool BS_hasDuration = true;
    [HideInInspector]
    public float BS_radius = 0f;
    [Header("Balls of Steel - 6")]
    public float BS_duration;

    [HideInInspector]
    public bool IS_hasDuration = true;
    [HideInInspector]
    public float IS_radius = 0f;
    [Header("I See Dead People - 7")]
    public float IS_duration;

    [HideInInspector]
    public bool CD_hasDuration = true;
    [Header("Calm Down - 8")]
    public float CD_duration;
    public float CD_radius;
    public float CD_speedMultiplier;
}
