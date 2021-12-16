using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnInfo : MonoBehaviour
{
    [SerializeField] public GameObject value;
    [SerializeField] public GameObject ButtonClickerGo;
    [SerializeField] public GameObject GameHandlerGo;
    [SerializeField] public int buttonNumber;
    [SerializeField] GameObject index;
    [SerializeField] GameObject clear;
    public bool isComplete = false;

    public void AddToValue(int valueAdd)
    {
        GameObject.Find("GameHandler").GetComponent<GameHandler>().sound.PlayMusic(0);
        int tempVal = int.Parse(value.GetComponent<TextMeshProUGUI>().text);
        tempVal += valueAdd;
        value.GetComponent<TextMeshProUGUI>().text = tempVal.ToString();
    }

    public void ClearValue()
    {
        value.GetComponent<TextMeshProUGUI>().text = 0.ToString();
    }

    private void Update()
    {
        ButtonClicker bcOb = ButtonClickerGo.GetComponent<ButtonClicker>();
        GameHandler ghOb = GameHandlerGo.GetComponent<GameHandler>();
        if (!isComplete)
        {
            if (buttonNumber == 0)
            {
                gameObject.GetComponent<Button>().interactable = true;
            }

            else if (int.Parse(bcOb.buttons[buttonNumber - 1].GetComponent<BtnInfo>().value.GetComponent<TextMeshProUGUI>().text) == ghOb.correctArrangement[ghOb.correctKeyList[buttonNumber - 1]])
            {
                gameObject.GetComponent<Button>().interactable = true;
            }

            else
                gameObject.GetComponent<Button>().interactable = false;

            if (int.Parse(bcOb.buttons[buttonNumber].GetComponent<BtnInfo>().value.GetComponent<TextMeshProUGUI>().text) == ghOb.correctArrangement[ghOb.correctKeyList[buttonNumber]])
            {
                index.GetComponent<Image>().color = Color.green;
                gameObject.GetComponent<Button>().interactable = false;

                if (buttonNumber == 4)
                    clear.SetActive(false);
            }

            else if (int.Parse(bcOb.buttons[buttonNumber].GetComponent<BtnInfo>().value.GetComponent<TextMeshProUGUI>().text) > ghOb.correctArrangement[ghOb.correctKeyList[buttonNumber]])
            {
                index.GetComponent<Image>().color = Color.red;
            }

            else
                index.GetComponent<Image>().color = Color.white;
        }
    }
}
