using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GameObject ReadyTimerGo;
    [SerializeField] public GameObject GivenTvText;
    [SerializeField] public GameObject GoalTvText;
    [SerializeField] public GameObject Timer;

    int timerInt = 1;

    public Dictionary<int, int> correctArrangement = new Dictionary<int, int>();
    public List<int> correctKeyList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        DefaultVal();
        SetActiveDefaults(false);
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
                GenerateGiven();
                StartCoroutine(TimerSet());
            }
            int count = int.Parse(ReadyTimerGo.GetComponent<TextMeshProUGUI>().text);
            count--;
            ReadyTimerGo.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TimerSet()
    {
        while (true)
        {
            Timer.GetComponent<TextMeshProUGUI>().text = $"TIMER\n{timerInt}";
            timerInt++;
            yield return new WaitForSeconds(1f);
        }
    }

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
        string collectedData = $"Array = [{correctArrangement[correctKeyList[0]]}, {correctArrangement[correctKeyList[1]]}, {correctArrangement[correctKeyList[2]]}, {correctArrangement[correctKeyList[3]]}, {correctArrangement[correctKeyList[4]]}]";
        string correctArrangementData = $"[{correctArrangement[0]}, {correctArrangement[1]}, {correctArrangement[2]}, {correctArrangement[3]}, {correctArrangement[4]}]";
        GivenTvText.GetComponent<TextMeshPro>().text = collectedData;
        GoalTvText.GetComponent<TextMeshPro>().text = correctArrangementData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
