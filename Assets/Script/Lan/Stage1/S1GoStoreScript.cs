using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1GoStoreScript : MonoBehaviour
{
    [SerializeField] public GameObject[] TvText;
    [SerializeField] public GameObject[] Platforms;
    [SerializeField] public GameObject player;

    public bool findPlayer = false;

    private void Start()
    {
        StartCoroutine(StartGameAfter());
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
                    player = GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().Player;
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

    IEnumerator StartGameAfter()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().MasterStart();
        StopAllCoroutines();
    }
}
