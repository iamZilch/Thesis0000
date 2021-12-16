using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage1Platform : MonoBehaviour
{
    [Header("Tutorial Stage 1 Script")]
    [SerializeField] GameObject script;
    [SerializeField] 
    public string value = "";

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.gameObject.tag.Equals("Player") && value.Equals(script.GetComponent<TutorialStage1Script>().correctAnswer))
        {
            //script.GetComponent<TutorialStage1Script>().DisableWrongAnswer();

        }*/

        if (other.gameObject.tag.Equals("Player"))
        {
            script.GetComponent<TutorialStage1Script>().chooseButton.SetActive(true);
            script.GetComponent<TutorialStage1Script>().platformValue = this.value;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            script.GetComponent<TutorialStage1Script>().chooseButton.SetActive(true);
            script.GetComponent<TutorialStage1Script>().platformValue = this.value;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            script.GetComponent<TutorialStage1Script>().chooseButton.SetActive(false);
            script.GetComponent<TutorialStage1Script>().platformValue = "";
        }
    }





}
