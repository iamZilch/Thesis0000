using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S4GameEngine : MonoBehaviour
{
    [SerializeField] GameObject ReadyTimerGo;
    [SerializeField] GameObject GivenGo;
    [SerializeField] GameObject PathGo;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject[] Answers;
    [SerializeField] GameObject TVText;
    [SerializeField] GameObject buttonHandlers;
    [SerializeField] GameObject Hint;
    [SerializeField] GameObject currQuestion;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject Player;
    [SerializeField] Transform[] checkpoints;
    [SerializeField] int checkpointNumber;
    [SerializeField] Button ulti;
    [SerializeField] Button fs;
    [SerializeField] TextMeshProUGUI ultitxt;
    [SerializeField] TextMeshProUGUI fstxt;

    public bool isOver = false;
    public int correctAnswer = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkpointNumber = 0;
        Player.GetComponent<SkillControls>().loadButtons(fs, ulti, fstxt, ultitxt);
        Timer.GetComponent<TextMeshProUGUI>().text = "0";
        DefaultUi(false);
        StartCoroutine(ReadyTimerNumerator());
    }

    public void DefaultUi(bool status)
    {
        ReadyTimerGo.SetActive(!status);
        GivenGo.SetActive(status);
        PathGo.SetActive(status);
        Timer.SetActive(status);
        buttonHandlers.SetActive(status);
        Hint.SetActive(status);
        currQuestion.SetActive(status);
        timer.SetActive(status);
    }

    IEnumerator ReadyTimerNumerator()
    {
        DefaultUi(false);
        while (true)
        {
            if (int.Parse(ReadyTimerGo.GetComponent<TextMeshProUGUI>().text) == 1)
            {
                StopAllCoroutines();
                GenerateGivenForLoop();
                DefaultUi(true);
                StartCoroutine(IncTimer());
            }
            int count = int.Parse(ReadyTimerGo.GetComponent<TextMeshProUGUI>().text);
            count--;
            GameObject.Find("Audio").GetComponent<S1SoundManager>().PlayMusic(1);
            ReadyTimerGo.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator IncTimer()
    {
        while (!isOver)
        {
            int count = int.Parse(Timer.GetComponent<TextMeshProUGUI>().text);
            count++;
            timeConsumed = count;
            GameObject.Find("Audio").GetComponent<S1SoundManager>().PlayMusic(3);
            Timer.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void GenerateGivenForLoop()
    {
        Dictionary<int, string> given = new Dictionary<int, string>
        {
            { 10, "If n = 0\nand you repeat \nn = n + 2, 5 times" },
            { 32, "If n = 2\nand you repeat \nn = n * 2, 4 times." },
            { 8, "If n = 1\nand you repeat \nn = n + n, 3 times. " },
            { 2, "If n = 10\nand you repeat \nn = n - 2, 4 times. " },
            { 16, "If n = 1\nand you repeat \nn = (n + 2) * 2, 2 times. " }
        };

        int[] keys = { 10, 32, 8, 2, 16 };
        int givenRan = Random.Range(0, keys.Length);
        correctAnswer = keys[givenRan];
        GivenGo.GetComponent<TextMeshProUGUI>().text = given[keys[givenRan]];
        Debug.Log(given[keys[givenRan]]);
        string tv = given[keys[givenRan]];
        int randCor = Random.Range(0, Answers.Length);
        for (int i = 0; i < Answers.Length; i++)
        {
            if (i == randCor)
            {
                Answers[i].GetComponent<TextMeshPro>().text = correctAnswer.ToString();
            }
            else
            {
                int toAdd = Random.Range(0, 10);
                Answers[i].GetComponent<TextMeshPro>().text = (correctAnswer + toAdd).ToString();
            }
        }
        TVText.GetComponent<TextMeshPro>().text = tv;
    }

    public void spawnCheckpoint()
    {
        Player.transform.position = checkpoints[checkpointNumber].position;
        GameObject.Find("Audio").GetComponent<S1SoundManager>().PlayMusic(0);
    }

    public void setCheckpoint(int x)
    {
        checkpointNumber = x;
    }

    int expGained;
    int timeConsumed;
    public int calculatePoints()
    {
        expGained = timeConsumed / 7;
        return 40 - expGained;
    }

    public void addPlayerExp()
    {
        if (calculatePoints() >= 0)
            GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += calculatePoints();
        GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
        // int points = int.Parse(calculatePoints().ToString());
        if ((calculatePoints() * 5) >= 0)
            GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += calculatePoints() * 5;
        SaveData.SaveDataProgress(Database.instance);
    }

    [SerializeField] GameObject endUI;
    [SerializeField] GameObject finishText;
    public void hideUI()
    {
        endUI.SetActive(true);
        int ppoints = 0;
        int ccoins = 0;

        if (!(calculatePoints() <= 0))
            ppoints = calculatePoints();

        if (!((calculatePoints() * 5) <= 0))
            ccoins = (calculatePoints() * 5);

        DefaultUi(false);
        ReadyTimerGo.SetActive(false);

        GameObject.Find("Audio").GetComponent<S1SoundManager>().PlayMusic(2);
        finishText.GetComponent<Text>().text = "Time Consumed: " + timeConsumed.ToString() +
                          "\nExp. Gained: " + ppoints.ToString() +
                          "\nCoins Earned: " + ccoins.ToString();
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }

}
