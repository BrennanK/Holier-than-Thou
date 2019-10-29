using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;

    private const string HAS_SAVED_GAME = "HAS_SAVED_GAME";
    private SaveData currentLoadedSaveData;
    public SaveData SaveDataInfo
    {
        get
        {
            return currentLoadedSaveData;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentLoadedSaveData = new SaveData();
        if(PlayerPrefs.HasKey(HAS_SAVED_GAME))
        {
            LoadSavedGame();
        }
        else
        {
            CreateSavedGame();
        }
    }

    public void IncrementSavedData(PlayerProfile matchPlayerProfile)
    {
        currentLoadedSaveData.playerProfile.IncrementProfileData(matchPlayerProfile);
        SaveGame();
    }

    private void CreateSavedGame()
    {
        PlayerPrefs.SetString(HAS_SAVED_GAME, JsonUtility.ToJson(currentLoadedSaveData));
    }

    private void SaveGame()
    {
        Debug.Log("Game Saved");
        PlayerPrefs.SetString(HAS_SAVED_GAME, JsonUtility.ToJson(currentLoadedSaveData));
    }

    private void LoadSavedGame()
    {
        Debug.Log("Loading Game");
        Debug.Log($"Loaded Game: {PlayerPrefs.GetString(HAS_SAVED_GAME)}");
        currentLoadedSaveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(HAS_SAVED_GAME));
    }

}
