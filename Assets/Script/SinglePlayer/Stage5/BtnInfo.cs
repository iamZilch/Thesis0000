using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnInfo : MonoBehaviour
{
    [SerializeField] public GameObject value;
    
    public void AddToValue(int valueAdd)
    {
        int tempVal = int.Parse(value.GetComponent<TextMeshProUGUI>().text);
        tempVal += valueAdd;
        value.GetComponent<TextMeshProUGUI>().text = tempVal.ToString();
    }

    public void ClearValue()
    {
        value.GetComponent<TextMeshProUGUI>().text = 0.ToString();
    }
}