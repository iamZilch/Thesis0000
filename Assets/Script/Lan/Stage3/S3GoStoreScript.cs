using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3GoStoreScript : MonoBehaviour
{
    [SerializeField] public GameObject ReadyTimerGo;
    [SerializeField] public GameObject GivenGo;
    [SerializeField] public GameObject PlayerCorrectAnswer;

    [Header("Stage1")]
    [SerializeField] public GameObject S1Bool1;
    [SerializeField] public GameObject S1Bool2;
    [SerializeField] public GameObject S1Bool3;
    [SerializeField] public GameObject S1Bool4;

    [Header("Stage2")]
    [SerializeField] public GameObject S2Bool1;
    [SerializeField] public GameObject S2Bool2;
    [SerializeField] public GameObject S2Bool3;

    [Header("Stage3")]
    [SerializeField] public GameObject S3Bool1;
    [SerializeField] public GameObject S3Bool2;
    [SerializeField] public GameObject S3Bool3;
    [SerializeField] public GameObject S3Bool4;

    [Header("Stage4")]
    [SerializeField] public GameObject S4Bool1;
    [SerializeField] public GameObject S4Bool2;
    [SerializeField] public GameObject S4Bool3;
    [SerializeField] public GameObject S4Bool4;

    [SerializeField] public GameObject player;

    [SerializeField] public GameObject[] PowerUpsSpawnPoint;

    public bool findPlayer = false;

    private void Start()
    {
        StartCoroutine(StartGameAfter());
    }

    IEnumerator StartGameAfter()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>().MasterStart();
        StopAllCoroutines();
    }

    public void enableCam()
    {
        StartCoroutine(findPlayerCor());
    }

    IEnumerator findPlayerCor()
    {
        while (!findPlayer)
        {
            try
            {
                if (player == null)
                {
                    player = GameObject.Find("Stage3Handler").GetComponent<LanStage3Handler>().Player;
                }
                if (player != null)
                {
                    player.GetComponent<PlayerLanExtension>().OnStageResetDefault();
                    findPlayer = true;
                }
            }
            catch (Exception e)
            { }
            yield return new WaitForSeconds(1f);
        }
    }
}
