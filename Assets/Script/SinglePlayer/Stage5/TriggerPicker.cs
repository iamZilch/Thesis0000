using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPicker : MonoBehaviour
{
    [SerializeField] public GameObject pickerHandler;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("s5Obs") && pickerHandler.GetComponent<ButtonClicker>().canPick)
        {
            pickerHandler.GetComponent<ButtonClicker>().canPick = false;
            ButtonClicker hand = pickerHandler.GetComponent<ButtonClicker>();
            hand.buttons[hand.activeBtn].GetComponent<BtnInfo>().AddToValue(other.GetComponent<ObjectScript>().Value);
            GameObject.Find("GameHandler").GetComponent<ObjectSpawner>().currentSpawn--;
            Destroy(other.gameObject);
        }
    }
}
