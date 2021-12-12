using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4GoStoreScript : MonoBehaviour
{
    [SerializeField] public GameObject ReadyTimerGo;
    [SerializeField] public GameObject GivenGo;
    [SerializeField] public GameObject PathGo;
    [SerializeField] public GameObject[] Answers;
    [SerializeField] public GameObject TVText;

    public bool isOver = false;
    public int correctAnswer = 0;

    private void Start()
    {
        StartCoroutine(StartGameAfter());
    }

    IEnumerator StartGameAfter()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Stage4Handler").GetComponent<LanStage4Handler>().MasterStart();
        StopAllCoroutines();
    }

}
