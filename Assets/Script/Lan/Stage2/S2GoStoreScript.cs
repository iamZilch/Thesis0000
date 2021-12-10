using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2GoStoreScript : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] public GameObject GivenTextGo;
    [SerializeField] public GameObject GivenTimerTextGo;
    [SerializeField] public GameObject CorrectAnswerTextGo;
    [SerializeField] public GameObject ReadyGivenTimerGo;
    [SerializeField] public GameObject QuestionNumberTextGo;
    [SerializeField] public GameObject[] PickupPointsGo;
    [SerializeField] public GameObject PickUpButton;

    //Parent
    [SerializeField] public GameObject GivenPanelParent;
    [SerializeField] public GameObject TimerPanelParent;
    [SerializeField] public GameObject CorrectAnswerPanelParent;

    GameObject handler;
    Transform handlerPos;
    bool handlerFound = false;
 
    public void PickUpTrigger()
    {
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().canPick = true;
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().PickUpButton.SetActive(false);
        GameObject.Find("SoundManager2").GetComponent<S2SoundManager>().PlayMusic(0);
    }

    private void Start()
    {
        StartCoroutine(findHandler());
        StartCoroutine(StartGameAfter());
    }

    IEnumerator findHandler()
    {
        while (!handlerFound)
        {
            try
            {
                handler = GameObject.Find("ButtonHandler");
                Debug.Log($"Pos for handler x:{handler.transform.position.x} -- y:{handler.transform.position.y} -- z:{handler.transform.position.z}");
                handlerPos = handler.transform;
                handler.transform.position = new Vector3(999f, 9999f, 9999f);
                handlerFound = true;
            }
            catch (Exception e)
            {
                handlerFound = false;
                StartCoroutine(findHandler());
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StartGameAfter()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().buttonHandler = handler;
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().handlerPos = handlerPos;
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().StartGame();
        StopAllCoroutines();
    }
}
