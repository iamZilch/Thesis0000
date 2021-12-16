using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlackHole : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] GameObject spawnPoint; //Lagay mo dito ung game object kung san mo sya gusto amg spawn pag nahulog
    [SerializeField] GameObject deadUi;
    GameObject current;


    [Header("Tutorial Stage 1 Script")]
    [SerializeField] GameObject script;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            //other.gameObject.transform.position = spawnPoint.transform.position;
            deadUi.SetActive(true);
            script.GetComponent<TutorialStage1Script>().controls.SetActive(false);
            current = other.gameObject;
          
        }
    }

    public void tryAgain()
    {
        current.gameObject.transform.position = spawnPoint.transform.position;
        script.GetComponent<TutorialStage1Script>().controls.SetActive(true);
        script.GetComponent<TutorialStage1Script>().ResetPlatforms();
        deadUi.SetActive(false);

    }


}
