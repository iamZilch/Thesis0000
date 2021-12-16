using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastPartCollider : MonoBehaviour
{
    [SerializeField] public GameObject bcOb;
    [SerializeField] public GameObject[] buttons;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            ButtonClicker bc = bcOb.GetComponent<ButtonClicker>();
            bc.EnterSubmit();
            bc.submitting = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            //buttonActivator(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            ButtonClicker bc = bcOb.GetComponent<ButtonClicker>();
            bc.ExitingSubmit();
            bc.submitting = false;
            //buttonActivator(false);
        }
    }

    void buttonActivator(bool status)
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].GetComponent<Button>().interactable = status;
    }
}
