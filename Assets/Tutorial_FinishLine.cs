using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_FinishLine : MonoBehaviour
{
    public GameObject congrats;
    public GameObject buttonHandler;
    public GameObject given;
    public AutoSave save;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Trix"))
        {
            congrats.SetActive(true);
            save.endTextTrigger();
            buttonHandler.SetActive(false);
            given.SetActive(false);
        }
    }
}
