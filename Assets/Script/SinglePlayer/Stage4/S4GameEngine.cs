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
    [SerializeField] GameObject currQuestion;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject Player;
    [SerializeField] Transform[] checkpoints;
    [SerializeField] int checkpointNumber;
    [SerializeField] Button ulti;
    [SerializeField] Button fs;
    [SerializeField] TextMeshProUGUI ultitxt;
    [SerializeField] TextMeshProUGUI fstxt;
    [SerializeField] GameObject[] loopFirst;
    [SerializeField] GameObject[] loopSecond;
    [SerializeField] GameObject[] loopThird;
    [SerializeField] GameObject[] loopFourth;
    [SerializeField] GameObject[] loopFifth;

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
            { 0, "If n = 0\n\tand you repeat \n\tn = n + 2, 5 times" },
            { 1, "If n = 2\n\tand you repeat \n\tn = n * 2, 5 times." },
            { 2, "If n = 1\n\tand you repeat \n\tn = n + n, 5 times. " },
            { 3, "If n = 10\n\tand you repeat \n\tn = n - 2, 5 times. " },
            { 4, "If n = 1\n\tand you repeat \n\tn = (n * 2) - n, 5 times. " }
        };

        Dictionary<int, int[]> myDic = new Dictionary<int, int[]>();
        myDic.Add(0, new int[] { 2, 4, 6, 8, 10 });
        myDic.Add(1, new int[] { 4, 8, 16, 32, 64 });
        myDic.Add(2, new int[] { 2, 4, 8, 16, 32 });
        myDic.Add(3, new int[] { 8, 6, 4, 2, 0 });
        myDic.Add(4, new int[] { 1, 1, 1, 1, 1 });

        int[] keys = { 0, 1, 2, 3, 4 };
        int givenRan = Random.Range(0, keys.Length);
        correctAnswer = keys[givenRan];
        Debug.Log($"Key : {correctAnswer}");
        GivenGo.GetComponent<TextMeshProUGUI>().text = given[keys[givenRan]];
        string tv = given[keys[givenRan]];
        TVText.GetComponent<TextMeshPro>().text = tv;

        GameObject[][] platforms = { loopFirst, loopSecond, loopThird, loopFourth, loopFifth };
        for (int i = 0; i < 5; i++)
        {
            for (int y = 0; y < 5; y++)
            {
                int[] correct = myDic[correctAnswer];
                platforms[i][y].GetComponent<S4PlatformScript>().correctAnswer = correct[i];
                platforms[i][y].GetComponent<S4PlatformScript>().StartCor();
            }
        }
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
