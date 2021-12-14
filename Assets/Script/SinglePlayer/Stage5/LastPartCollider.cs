using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPartCollider : MonoBehaviour
{
    [SerializeField] public GameObject bcOb;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            ButtonClicker bc = bcOb.GetComponent<ButtonClicker>();
            bc.EnterSubmit();
            bc.submitting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            ButtonClicker bc = bcOb.GetComponent<ButtonClicker>();
            bc.ExitingSubmit();
            bc.submitting = false;
        }
        
    }
}
