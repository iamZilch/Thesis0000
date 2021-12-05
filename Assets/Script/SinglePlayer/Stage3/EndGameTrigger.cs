using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour
{
    [Header("Stage 3 Handler")]
    [SerializeField] GameObject main;
    [SerializeField] GameObject TimerFinishText;
    [SerializeField] GameObject finishCanvas;
    [SerializeField] GameObject finishText;
    [SerializeField] GameObject timerPanel;
    [SerializeField] GameObject buttonHandler;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            main.GetComponent<Stage3ScriptHandler>().isFinish = true;
            Debug.Log($"Game is finished : {TimerFinishText.GetComponent<TextMeshProUGUI>().text}");
            buttonHandler.SetActive(false);
            timerPanel.SetActive(false);
            finishCanvas.SetActive(true);
            main.GetComponent<Stage3ScriptHandler>().addPlayerExp();
            main.GetComponent<Stage3ScriptHandler>().StopAllCoroutines();
            //LAHAT NG DATA NA GUSTO MONG IPASOK DITO MO ILAGAY!
            main.GetComponent<Stage3ScriptHandler>().endgame();
        }
    }
}
