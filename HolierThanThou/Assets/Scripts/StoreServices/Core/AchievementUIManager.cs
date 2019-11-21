using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class AchievementUIManager : MonoBehaviour
{
    [Header("UI Integration")]
    public GameObject prefabEntry;
    public AchievementUI[] achievementsInUI;




    // Start is called before the first frame update
    void Start()
    {
        StoreServices.Core.Achievements.AchievementInstance[] achievementInstances = StoreServices.AchievementManager.instance.AchievementInstances.OrderByDescending(a => a.Claimable).ThenBy(a => a.AlreadyCompleted).ThenByDescending(a =>a.ProgressInPercentage).ToArray();



        if(achievementInstances.Length == achievementsInUI.Length)
        {
            for(int i = 0; i < achievementsInUI.Length; i++)
            {
                float percentageProgress = achievementInstances[i].ProgressInPercentage;
                percentageProgress *= 100;
                if (achievementInstances[i].AlreadyClaimed)
                {
                    achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, "Claimed", achievementInstances[i].AchievementInternalID, achievementInstances[i].Reward);
                }
                else if (achievementInstances[i].Claimable && !achievementInstances[i].AlreadyClaimed)
                {
                    achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, $"{achievementInstances[i].Reward}<sprite=0>", achievementInstances[i].AchievementInternalID, achievementInstances[i].Reward);
                }
                else
                {
                    achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, $"{Mathf.Round(percentageProgress)}%", achievementInstances[i].AchievementInternalID, achievementInstances[i].Reward);
                }
                
            }
        }

    }

    public void UpdateAllAchievements()
    {
        StoreServices.Core.Achievements.AchievementInstance[] achievementInstances = StoreServices.AchievementManager.instance.AchievementInstances.OrderByDescending(a => a.Claimable).ThenBy(a => a.AlreadyCompleted).ThenByDescending(a => a.ProgressInPercentage).ToArray();



        if (achievementInstances.Length == achievementsInUI.Length)
        {
            for (int i = 0; i < achievementsInUI.Length; i++)
            {
                float percentageProgress = achievementInstances[i].ProgressInPercentage;
                percentageProgress *= 100;
                if (achievementInstances[i].AlreadyClaimed)
                {
                    achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, "Claimed",achievementInstances[i].AchievementInternalID, achievementInstances[i].Reward);
                }
                else if (achievementInstances[i].Claimable && !achievementInstances[i].AlreadyClaimed)
                {
                    achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, $"{achievementInstances[i].Reward}<sprite=0>", achievementInstances[i].AchievementInternalID, achievementInstances[i].Reward);
                    
                }
                else
                {
                    achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, $"{Mathf.Round(percentageProgress)}%",achievementInstances[i].AchievementInternalID, achievementInstances[i].Reward);
                }

            }
        }
    }



    public void ShowAchievements()
    {
        //(Sprite)AssetDatabase.LoadAssetAtPath("Assets / Art / Menu UI / Poly - symbol.png", typeof(Sprite));
        //prefabCanvas.SetActive(true);
    }

    public void HideAchievement()
    {
        //prefabCanvas.SetActive(false);
    }
}
