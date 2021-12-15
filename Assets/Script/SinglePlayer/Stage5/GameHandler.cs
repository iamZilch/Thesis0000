using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject ReadyTimerGo;
    [SerializeField] public GameObject GivenTvText;
    [SerializeField] public GameObject GoalTvText;
    [SerializeField] public GameObject Timer;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject[] buttons;
    [SerializeField] public S2SoundManager sound;
    [SerializeField] Button ulti;
    [SerializeField] Button fs;
    [SerializeField] TextMeshProUGUI ultitxt;
    [SerializeField] TextMeshProUGUI fstxt;
    [SerializeField] bool onTutorial;

    int timerInt = 1;

    int expGained;
    int timeConsumed;

    public Dictionary<int, int> correctArrangement = new Dictionary<int, int>();
    public List<int> correctKeyList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {

        Player.GetComponent<SkillControls>().loadButtons(fs, ulti, fstxt, ultitxt);
        DefaultVal();
        disableButtons(false);
        SetActiveDefaults(false);
        startGame();
    }

    void startGame()
    {
        StartCoroutine(ReadyStartGoNum());
    }

    public void DefaultVal()
    {
        ReadyTimerGo.GetComponent<TextMeshProUGUI>().text = "4";
        GivenTvText.GetComponent<TextMeshPro>().text = "";
        GoalTvText.GetComponent<TextMeshPro>().text = "";
        Timer.GetComponent<TextMeshProUGUI>().text = "TIMER\n0";
    }

    public void SetActiveDefaults(bool status)
    {
        ReadyTimerGo.SetActive(!status);
        GivenTvText.SetActive(status);
        GoalTvText.SetActive(status);
        Timer.SetActive(status);
    }

    public void StartGame()
    {

    }

    IEnumerator ReadyStartGoNum()
    {
        while (int.Parse(ReadyTimerGo.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if (int.Parse(ReadyTimerGo.GetComponent<TextMeshProUGUI>().text) == 1)
            {
                SetActiveDefaults(true);
                disableButtons(true);
                GenerateGiven();
                StartCoroutine(TimerSet());
            }
            int count = int.Parse(ReadyTimerGo.GetComponent<TextMeshProUGUI>().text);
            count--;
            sound.PlayMusic(1);
            ReadyTimerGo.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TimerSet()
    {
        while (true)
        {
            sound.PlayMusic(3);
            Timer.GetComponent<TextMeshProUGUI>().text = $"{timerInt}";
            timerInt++;
            if (!isFinished)
                timeConsumed = timerInt;
            yield return new WaitForSeconds(1f);
        }
    }
    public List<int> index = new List<int>();
    public void GenerateGiven()
    {
        correctArrangement.Clear();
        for (int i = 0; i < 5; i++)
        {
            correctArrangement.Add(i, Random.Range(1, 20));
        }
        List<int> keyList = new List<int>
        {
            0,
            1,
            2,
            3,
            4
        };
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(0, keyList.Count);
            correctKeyList.Add(keyList[ran]);
            keyList.Remove(keyList[ran]);
        }
        string collectedData = $"Array Value\n{{{correctArrangement[correctKeyList[0]]}, {correctArrangement[correctKeyList[1]]}, {correctArrangement[correctKeyList[2]]}, {correctArrangement[correctKeyList[3]]}, {correctArrangement[correctKeyList[4]]}}}";
        // string correctArrangementData = $"[{correctArrangement[0]}, {correctArrangement[1]}, {correctArrangement[2]}, {correctArrangement[3]}, {correctArrangement[4]}]";
        string correctArrangementData = $"Index\n[{correctKeyList[0]}], [{correctKeyList[1]}], [{correctKeyList[2]}], [{correctKeyList[3]}], [{correctKeyList[4]}]";
        Debug.Log($"{correctKeyList[0]} , {correctKeyList[1]}, {correctKeyList[2]}, {correctKeyList[3]}, {correctKeyList[4]}");
        GivenTvText.GetComponent<TextMeshPro>().text = collectedData;
        GoalTvText.GetComponent<TextMeshPro>().text = correctArrangementData;
    }

    public void spawnCheckpoint()
    {
        Player.transform.position = spawnPoint.position;
    }

    public int calculatePoints()
    {
        expGained = timeConsumed / 4;
        return 60 - expGained;
    }

    public void addPlayerExp()
    {
        if (calculatePoints() >= 0)
            GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += calculatePoints();
        GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
        if ((calculatePoints() * 5) >= 0)
            GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += calculatePoints() * 5;
        SaveData.SaveDataProgress(Database.instance);
    }

    [SerializeField] GameObject endUI;
    [SerializeField] GameObject finishText;
    bool isFinished = false;
    public void hideUI()
    {
        sound.PlayMusic(2);
        isFinished = true;
        endUI.SetActive(true);
        int ppoints = 0;
        int ccoins = 0;
        SetActiveDefaults(false);

        if (!(calculatePoints() <= 0))
            ppoints = calculatePoints();

        if (!((calculatePoints() * 5) <= 0))
            ccoins = (calculatePoints() * 5);

        ReadyTimerGo.SetActive(false);
        disableButtons(false);

        // GameObject.Find("Audio").GetComponent<S1SoundManager>().PlayMusic(2);
        if (!onTutorial)
            finishText.GetComponent<Text>().text = "Time Consumed: " + timeConsumed.ToString() +
                              "\nExp. Gained: " + ppoints.ToString() +
                              "\nCoins Earned: " + ccoins.ToString();
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }

    public void disableButtons(bool status)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(status);
        }
    }
}
