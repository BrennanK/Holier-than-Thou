using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementButton : MonoBehaviour
{
    private AchievementUI achievementUI;


    private void Start()
    {
        achievementUI = gameObject.GetComponent<AchievementUI>();

    }

    public void OnClickClaim()
    {
        StoreServices.AchievementManager.instance.ClaimAchievement(achievementUI.achievementID, achievementUI.reward);

    }
}
