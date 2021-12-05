using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScriptHandler : MonoBehaviour
{
    [Header("Player UI Info")]
    [SerializeField] GameObject PlayerNameText;
    [SerializeField] GameObject PlayerMoneyText;
    [SerializeField] GameObject PlayerLevelText;
    [SerializeField] GameObject PlayerProgressBar;
    [SerializeField] GameObject playerText;
    GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNameText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerName;
        playerText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerName;
        PlayerMoneyText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerMoney.ToString();
        PlayerLevelText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerLevel.ToString();
        main = GameObject.Find("Opening_Game_Script");
    }

    public void initPenguin(int method)
    {
        if (method == 1)
        {
            CharacterSelection.instance.InitPenguin(Database.instance.UsedCharacter);
        }
        else
        {
            CharacterSelection.instance.InitPenguin(-1);
        }
    }

    public void loadQuizMode()
    {
        SceneManager.LoadScene("QuizMode");
    }

    public void stopMusic()
    {
        main.GetComponent<SoundScript>().stopMusic();
    }

    public void changeAccount()
    {
        main.SetActive(false);
    }
}
