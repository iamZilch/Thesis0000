using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProgress : MonoBehaviour
{
    public static SaveProgress instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePlayerData(string playerName, int playerMoney, int playerLevel, float playerCurrentExp, float playerLevelCapacity, bool[] UnlockedStages, bool[] UnlockedCharacter, int UseCharacter, int[] tutorialCheckpoints, bool[] unlockedTutorials)
    {
        Database.instance.playerName = playerName;
        Database.instance.playerMoney = playerMoney;
        Database.instance.playerLevel = playerLevel;
        Database.instance.playerCurrentExp = playerCurrentExp;
        Database.instance.playerLevelCapacity = playerLevelCapacity;
        Database.instance.UnlockedStages = UnlockedStages;
        Database.instance.UnlockCharacter = UnlockedCharacter;
        Database.instance.UsedCharacter = UseCharacter;
        Database.instance.tutorialCheckpoints = tutorialCheckpoints;
        Database.instance.unlockedTutorials = unlockedTutorials;

        SaveData.SaveDataProgress(Database.instance);
    }
}
