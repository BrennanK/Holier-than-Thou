using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public PlayerProfile playerProfile;

    //Stores Achievement things
    public List<string[]> AchievementInfo;

    public SaveData()
    {
        playerProfile = new PlayerProfile();
    }
}