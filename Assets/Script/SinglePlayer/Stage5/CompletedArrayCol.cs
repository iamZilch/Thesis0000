using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompletedArrayCol : MonoBehaviour
{
    [SerializeField] public GameObject wrong;
    [SerializeField] public GameObject ButtonClickerGo;
    [SerializeField] public GameObject GameHandlerGo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Maze"))
        {
            ButtonClicker bcOb = ButtonClickerGo.GetComponent<ButtonClicker>();
            GameHandler ghOb = GameHandlerGo.GetComponent<GameHandler>();
            bool correct = true;
            for(int i = 0; i < bcOb.buttons.Length; i++)
            {
                if(int.Parse(bcOb.buttons[i].GetComponent<BtnInfo>().value.GetComponent<TextMeshProUGUI>().text) != ghOb.correctArrangement[ghOb.correctKeyList[i]])
                {
                    correct = false;
                    break;
                }
            }
            if (correct)
            {
                wrong.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wrong.SetActive(true);
    }
}
