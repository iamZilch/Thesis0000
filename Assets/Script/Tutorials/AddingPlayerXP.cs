using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddingPlayerXP : MonoBehaviour
{
    // Start is called before the first frame update
    public void addPlayerExp()
    {
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += 10;
        GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += 20;
        SaveData.SaveDataProgress(Database.instance);
        SceneManager.LoadScene("Main_Menu_Scene");
    }

}
