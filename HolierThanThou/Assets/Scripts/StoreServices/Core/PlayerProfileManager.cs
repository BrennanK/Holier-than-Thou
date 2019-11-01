using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public GameObject playerProfileCanvas;
    public TextMeshProUGUI[] profileEntryTexts;

    private void Start()
    {
        HidePlayerProfile();
    }

    private void UpdatePlayerProfile()
    {
        // TODO currently this is hard coded and it is kinda bad
        if (profileEntryTexts.Length != 5)
        {
            return;
        }

        SaveData saveData = SaveGameManager.instance.SaveDataInfo;
        profileEntryTexts[0].text = $"<color=#814F2F>Games Played:</color> {saveData.playerProfile.gamesPlayed}";
        profileEntryTexts[1].text = $"<color=#814F2F>Time Played:</color> {saveData.playerProfile.gamesWon}";

    }

    public void ShowPlayerProfile()
    {
        UpdatePlayerProfile();
        playerProfileCanvas.SetActive(true);
    }

    public void HidePlayerProfile()
    {
        playerProfileCanvas.SetActive(false);
    }
}