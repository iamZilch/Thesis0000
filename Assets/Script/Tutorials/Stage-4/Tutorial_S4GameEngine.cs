using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_S4GameEngine : MonoBehaviour
{
    [SerializeField] GameObject ReadyTimerGo;
    [SerializeField] GameObject GivenGo;
    [SerializeField] GameObject PathGo;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject[] Answers;

    public bool isOver = false;
    public int correctAnswer = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            Timer.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void GenerateGivenForLoop()
    {
        Dictionary<int, string> given = new Dictionary<int, string>
        {
            { 10, "If n = 0\n\tand you repeat n = n + 2, 5 times" }
        };

        int[] keys = { 10 };
        int givenRan = Random.Range(0, keys.Length);
        correctAnswer = keys[givenRan];
        GivenGo.GetComponent<TextMeshProUGUI>().text = given[keys[givenRan]];
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
    }
}
