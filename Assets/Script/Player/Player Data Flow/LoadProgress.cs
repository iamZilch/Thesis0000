using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadProgress : MonoBehaviour
{
    public static LoadProgress instance;

    [Header("Player Data Prefab GameObject")]
    [SerializeField] GameObject PlayerDataPrefab;
    [SerializeField] GameObject PlayerDataPrefabPanel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void InstantiatePlayerData(string PlayerName)
    {
        SaveData.LoadData();
        List<PlayerData> PlayerDataList = SaveData.GetPlayerDataList();
        foreach (PlayerData player in PlayerDataList)
        {
            if (player.playerName.Equals(PlayerName))
            {
                Debug.Log($"Executing loaded player data ... {player.UsedCharacter}  Name: {player.playerName}");
                Database.instance.playerName = player.playerName;
                Database.instance.playerMoney = player.playerMoney;
                Database.instance.playerLevelCapacity = player.playerLevelCapacity;
                Database.instance.playerLevel = player.playerLevel;
                Database.instance.playerCurrentExp = player.playerCurrentExp;
                Database.instance.UsedCharacter = player.UsedCharacter;
                Database.instance.UnlockCharacter = player.UnlockCharacter;
                Database.instance.UnlockedStages = player.UnlockedStages;
                Database.instance.tutorialCheckpoints = player.tutorialCheckpoints;
                Database.instance.unlockedTutorials = player.unlockedTutorials;
                break;
            }
        }
    }

    public void ChangeName(string PlayerName, string newName) //change player name
    {
        List<PlayerData> PlayerDataList = SaveData.GetPlayerDataList();
        foreach (PlayerData player in PlayerDataList)
        {
            if (player.playerName.Equals(PlayerName))
            {
                SaveData.ChangeName(Database.instance, newName);
                break;
            }
        }
    }

    public void deletePlayer(string PlayerName) //change player name
    {
        List<PlayerData> PlayerDataList = SaveData.GetPlayerDataList();
        foreach (PlayerData player in PlayerDataList)
        {
            if (player.playerName.Equals(PlayerName))
            {
                Debug.Log($"{PlayerDataList.Count}");
                SaveData.DeleteAccount(Database.instance);
                break;
            }
        }
    }

    public void SpawnExistingUser()
    {
        foreach (Transform child in PlayerDataPrefabPanel.transform)
        {
            Destroy(child.gameObject);
        }
        SaveData.LoadData();
        List<PlayerData> PlayerDataList = SaveData.GetPlayerDataList();
        foreach (PlayerData PlayerDataItem in PlayerDataList)
        {
            GameObject player = PlayerDataPrefab;
            player.GetComponent<PlayerDataPrefab>().SetPrefabNameText(PlayerDataItem.playerName);
            Instantiate(player, PlayerDataPrefabPanel.transform);
        }
    }
}
