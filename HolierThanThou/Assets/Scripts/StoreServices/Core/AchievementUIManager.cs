using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;

public class AchievementUIManager : MonoBehaviour
{
    [Header("UI Integration")]
    public GameObject prefabEntry;
    public GameObject prefabCanvas;
    public AchievementUI[] achievementsInUI;




    // Start is called before the first frame update
    void Start()
    {
        StoreServices.Core.Achievements.AchievementInstance[] achievementInstances = StoreServices.AchievementManager.instance.AchievementInstances.OrderBy(a => a.AlreadyCompleted).ThenByDescending(a =>a.ProgressInPercentage).ToArray();



        if(achievementInstances.Length == achievementsInUI.Length)
        {
            for(int i = 0; i < achievementsInUI.Length; i++)
            {
                float percentageProgress = achievementInstances[i].ProgressInPercentage;
                percentageProgress *= 100;
                achievementsInUI[i].UpdateAchievement(achievementInstances[i].AchievementName, achievementInstances[i].AchievementDescription, $"{Mathf.Round(percentageProgress)}%");
            }
        }
        else
        {
            Debug.Log($"Achievements on game and achievements on UI are not the same!");
        }
        prefabCanvas.SetActive(false);
    }

    public void ShowAchievements()
    {
        prefabCanvas.SetActive(true);
    }

    public void HideAchievement()
    {
        prefabCanvas.SetActive(false);
    }
}
