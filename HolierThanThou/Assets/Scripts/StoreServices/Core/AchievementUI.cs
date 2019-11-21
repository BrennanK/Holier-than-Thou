using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public Image acheivementBackgroundImage;
    public TextMeshProUGUI achievementName;
    public TextMeshProUGUI achievementDescription;
    public TextMeshProUGUI achievementCompletion;
    public string achievementID;
    public int reward;

    public void UpdateAchievement(string _name, string _description, string _completion, string _achievementID, int _reward)
    {
        achievementName.text = _name;
        achievementDescription.text = _description;
        achievementCompletion.text = _completion;
        achievementID = _achievementID;
        reward = _reward;
    }
}
