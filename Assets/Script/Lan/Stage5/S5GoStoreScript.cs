using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S5GoStoreScript : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject Stage5Handler;
    [SerializeField] public GameObject GivenTvText;
    [SerializeField] public GameObject GoalTvText;


    public bool findPlayer = false;
    public bool findStageHandler = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(findStageHandlerCor());
    }

    IEnumerator findStageHandlerCor()
    {
        while (!findStageHandler)
        {
            try
            {
                if (Stage5Handler == null)
                {
                    Stage5Handler = GameObject.Find("Stage5Handler");
                }
                if (Stage5Handler != null)
                {
                    Stage5Handler.GetComponent<LanStage5Handler>().pickUP.SetActive(false);
                    findStageHandler = true;
                }
            }
            catch (Exception e)
            { }
            yield return new WaitForSeconds(1f);
        }
    }

    public void SpawnShits()
    {
        GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().MasterStart();
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
                if(player == null)
                {
                    player = GameObject.Find("Stage5Handler").GetComponent<LanStage5Handler>().player;
                }
                if(player != null)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
