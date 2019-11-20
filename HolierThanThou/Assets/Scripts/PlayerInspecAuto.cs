using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerInspecAuto : MonoBehaviour
{
    PowerUpTracker powerUpTracker;
    Competitor competitor;
    LoadCamera loadCamera;
    PlayerUIScript playerUIScript;
    void Awake()
    {
        powerUpTracker = transform.GetComponent<PowerUpTracker>();
        competitor = transform.GetComponent<Competitor>();
        loadCamera = transform.GetComponent<LoadCamera>();
        playerUIScript = transform.GetComponent<PlayerUIScript>();
    }

    void Start()
    {
        powerUpTracker.powerupImages[0] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/BlastZone.png", typeof(Sprite));
        powerUpTracker.powerupImages[1] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/ChillOut.png", typeof(Sprite));
        powerUpTracker.powerupImages[2] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/GottaGoFast.png", typeof(Sprite));
        powerUpTracker.powerupImages[3] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/CantTouchThis.png", typeof(Sprite));
        powerUpTracker.powerupImages[4] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/SneakySnake.png", typeof(Sprite));
        powerUpTracker.powerupImages[5] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/KettleBell.png", typeof(Sprite));
        powerUpTracker.powerupImages[6] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/Super_bounce.png", typeof(Sprite));
        powerUpTracker.powerupImages[7] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/BlastZone.png", typeof(Sprite));
        powerUpTracker.powerupImages[8] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/Dis_Mine.png", typeof(Sprite));
        powerUpTracker.powerupImages[9] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/PowerUp Icons/PowerUpBackground.png", typeof(Sprite));

    }
}
