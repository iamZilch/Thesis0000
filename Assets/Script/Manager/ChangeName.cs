using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeName : MonoBehaviour
{
    [SerializeField] GameObject playerNameText;

    public void changeName()
    {
        if (!playerNameText.GetComponent<TextMeshProUGUI>().text.Equals(null))
        {
            string currName;
            string newName;
            Debug.Log("Executing create player...");
            newName = playerNameText.GetComponent<TextMeshProUGUI>().text;
            currName = GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerName;
            GameObject.Find("Opening_Game_Script").GetComponent<LoadProgress>().ChangeName(currName, newName);
        }
        else
        {
            Debug.Log("Please Input username");
            //Message box
        }
    }

    public void removeAccount()
    {
        GameObject.Find("Opening_Game_Script").GetComponent<LoadProgress>().deletePlayer(GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerName);
    }
}
