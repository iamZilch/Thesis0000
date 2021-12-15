using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName = null;
    public int playerMoney = 0;
    public int playerLevel = 1;
    public float playerCurrentExp = 0;
    public float playerLevelCapacity = 10;
    public bool[] UnlockedStages = { true, false, false, false, false };
    public bool[] UnlockCharacter = { true, false, false, true, false, false, true, false, false};
    public int[] tutorialCheckpoints = { 0, 0, 0, 0, 0, 0 };
    public bool[] unlockedTutorials = { false, false, false, false, false, false };
    public int UsedCharacter = 0;

    public PlayerData(Database data)
    {
        playerName = data.playerName;
        playerMoney = data.playerMoney;
        playerLevel = data.playerLevel;
        playerCurrentExp = data.playerCurrentExp;
        playerLevelCapacity = data.playerLevelCapacity;
        UnlockedStages = data.UnlockedStages;
        UnlockCharacter = data.UnlockCharacter;
        UsedCharacter = data.UsedCharacter;
        tutorialCheckpoints = data.tutorialCheckpoints;
        unlockedTutorials = data.unlockedTutorials;
    }
}
