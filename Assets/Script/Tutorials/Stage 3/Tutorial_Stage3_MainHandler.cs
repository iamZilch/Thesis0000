using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial_Stage3_MainHandler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnPoint;

    [Header("Player UI")]
    [SerializeField] GameObject DisplayGivenText;
    [SerializeField] GameObject StartTimerText;
    [SerializeField] GameObject TimerFinishText;
    [SerializeField] GameObject ButtonHandler;
    [SerializeField] TextMeshProUGUI firstSkillBtnText;
    [SerializeField] Button firstSkillButton;

    [SerializeField] TextMeshProUGUI ultiSkillBtnText;
    [SerializeField] Button ultiSkillButton;

    [Header("Game Info")]
    public string correctAnswer = "";

    [Header("Check Points")]
    [SerializeField] public GameObject CheckPointGO;

    public bool isFinish = false;
    int timeConsumed;


    // Start is called before the first frame update
    void Start()
    {
        Player.transform.position = SpawnPoint.transform.position;
        Player.SetActive(true);
        Tutorial_Stage_3Given.InitializeGiven();
        LoadDefault();
        StartGame();
    }

    public GameObject getPlayer()
    {
        return this.Player;
    }

    public void StartGame()
    {
        //LoadDefaults
        //Start ReadyTimerCoroutine
        StartCoroutine(ReadyTimerCoroutine());
    }

    public void GenerateGiven()
    {
        DisplayGivenText.SetActive(true);
        string given = "";
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            correctAnswer = "T";
            int size = Tutorial_Stage_3Given.TrueGiven.Count;
            given = Tutorial_Stage_3Given.TrueGiven[Random.Range(0, size)];
        }
        else
        {
            correctAnswer = "F";
            int size = Tutorial_Stage_3Given.FalseGiven.Count;
            given = Tutorial_Stage_3Given.FalseGiven[Random.Range(0, size)];
        }
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = given;
    }

    IEnumerator ReadyTimerCoroutine()
    {
        StartTimerText.SetActive(true);
        while (int.Parse(StartTimerText.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if (int.Parse(StartTimerText.GetComponent<TextMeshProUGUI>().text) == 1)
            {
                StopAllCoroutines();
                StartTimerText.SetActive(false);
                //Generate Given
                GenerateGiven();
                //GameObject.Find("SoundManager").GetComponent<S3SoundManager>().PlayMusic(1);
                //Start Timer Text To finish
                StartCoroutine(TimerFinishTextCoroutine());
                ButtonHandler.SetActive(true);

            }
            int newInt = int.Parse(StartTimerText.GetComponent<TextMeshProUGUI>().text);
            newInt--;
            StartTimerText.GetComponent<TextMeshProUGUI>().text = newInt.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TimerFinishTextCoroutine()
    {
        TimerFinishText.SetActive(true);
        while (!isFinish)
        {
            //GameObject.Find("SoundManager").GetComponent<S3SoundManager>().PlayMusic(3);
            int newInt = int.Parse(TimerFinishText.GetComponent<TextMeshProUGUI>().text);
            newInt++;
            timeConsumed = newInt;
            TimerFinishText.GetComponent<TextMeshProUGUI>().text = newInt.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void LoadDefault()
    {
        SetUiActive(false);
        loadButtons();
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "";
        StartTimerText.GetComponent<TextMeshProUGUI>().text = "4";
        TimerFinishText.GetComponent<TextMeshProUGUI>().text = "0";
    }

    public void SetUiActive(bool status)
    {
        DisplayGivenText.SetActive(status);
        StartTimerText.SetActive(status);
        TimerFinishText.SetActive(status);
        ButtonHandler.SetActive(status);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.transform.position = CheckPointGO.transform.position;
        }
    }

    public void loadButtons()
    {
        Player.GetComponent<SkillControls>().loadButtons(firstSkillButton, ultiSkillButton, firstSkillBtnText, ultiSkillBtnText);
        ButtonHandler.GetComponent<ButtonsHandler>().setPlayer(Player);
        firstSkillButton.gameObject.SetActive(false);
        ultiSkillButton.interactable = false;

    }

    public int generateScore()
    {
        if (timeConsumed / 8 >= 0)
            return timeConsumed / 8;

        else
            return 0;
    }

    public void addPlayerExp()
    {
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += 10;
        GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += 20;
        SaveData.SaveDataProgress(Database.instance);
    }

    [SerializeField] GameObject End;
    [SerializeField] GameObject endText;
    public void endgame()
    {
        endText.SetActive(true); SetUiActive(false);
        endText.GetComponent<Text>().text = "Congratulations! \nYou earned : " + generateScore() * 2 + "Exp. Points" + "\n\t" + generateScore() * 5 + "coins.";
    }

}
