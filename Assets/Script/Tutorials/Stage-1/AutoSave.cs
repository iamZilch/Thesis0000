using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoSave : MonoBehaviour
{

    bool hasCollided;
    [SerializeField] int checkpointNumber;
    [SerializeField] int stageNumber;
    [SerializeField] int exp;
    [SerializeField] int coins;
    [SerializeField] GameObject endText;

    private void Start()
    {
        Debug.Log("You are on : Stage " + stageNumber);
        Debug.Log("Exp to gain : " + exp);
        Debug.Log("Coins to gain : " + coins);
        Debug.Log("Last Checkpoint : " + GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[stageNumber]);
    }

    public void setCheckpoint(int checkpoint)
    {
        Debug.Log("Checkpoint set : " + checkpoint);
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().tutorialCheckpoints[stageNumber] = checkpoint;
        SaveData.SaveDataProgress(Database.instance);
    }

    public void endTextTrigger()
    {
        if (GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[stageNumber])
            endText.GetComponent<Text>().text = "Thank you for retaking the tutorial!\n ";
        endText.SetActive(true);
    }

    public void endPhase(int stageNumber)
    {
        if (!GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[stageNumber])
        {
            GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += exp;
            GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
            GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += coins;
        }

        else
            Debug.Log("No earnings.");

        GameObject.Find("Opening_Game_Script").GetComponent<Database>().unlockedTutorials[stageNumber] = true;

        SceneManager.LoadScene("Main_Menu_Scene");
        SaveData.SaveDataProgress(Database.instance);
    }
}
