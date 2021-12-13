using System;
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

    public bool isClean = false;


    // Start is called before the first frame update
    void Start()
    {
        isClean = false;
        StartCoroutine(cleanScene());
        PlayerNameText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerName;
        playerText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerName;
        PlayerMoneyText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerMoney.ToString();
        PlayerLevelText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerLevel.ToString();
        main = GameObject.Find("Opening_Game_Script");
    }

    IEnumerator cleanScene()
    {
        while (!isClean)
        {
            try
            {
                Destroy(GameObject.Find("NetworkManager"));
                Destroy(GameObject.Find("UI Canvas"));
                //Destroy(GameObject.Find("~LeanTween"));
                Destroy(GameObject.Find("Stage1Handler"));
                Destroy(GameObject.Find("Stage2Handler"));
                Destroy(GameObject.Find("Stage3Handler"));
                Destroy(GameObject.Find("Stage4Handler"));
                Destroy(GameObject.Find("NetworkStorage"));
                isClean = true;
            }
            catch (Exception e)
            {

            }
            yield return new WaitForSeconds(1f);
        }
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
