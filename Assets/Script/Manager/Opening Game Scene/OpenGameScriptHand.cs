using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGameScriptHand : MonoBehaviour
{
    [SerializeField] GameObject playerNameText;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void CreatePlayer()
    {
        if (!playerNameText.GetComponent<TextMeshProUGUI>().text.Equals(null))
        {
            Debug.Log("Executing create player...");
            Database.instance.playerName = playerNameText.GetComponent<TextMeshProUGUI>().text;
            CreatePlayerData.CreatePlayer(Database.instance);
            SceneManager.LoadScene("IntroToControls_Mechanics");
        }
        else
        {
            Debug.Log("Please Input username");
            //Message box
        }

    }
}
