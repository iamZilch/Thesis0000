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
 
    public void PickUpTrigger()
    {
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().canPick = true;
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().PickUpButton.SetActive(false);
        GameObject.Find("SoundManager2").GetComponent<S2SoundManager>().PlayMusic(0);
    }

    private void Start()
    {
        StartCoroutine(StartGameAfter());
    }

    IEnumerator StartGameAfter()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().StartGame();
        StopAllCoroutines();
    }
}
