using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicker : MonoBehaviour
{
    [SerializeField] public GameObject gameHandlerGo;
    [SerializeField] public GameObject[] buttons;
    [SerializeField] public GameObject winnerSpawn;
    [SerializeField] public GameObject player;
    public bool canPick = false;
    public bool submitting = false;
    public bool allCorrect = true;
    public int activeBtn = 0;
    public int index = 0;
    public bool init = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateBtn(int refInt)
    {
        if (!submitting)
        {
            activeBtn = refInt;
            canPick = true;
        }
        else if(submitting)
        {
            activeBtn = refInt;
            GameHandler ghOb = gameHandlerGo.GetComponent<GameHandler>();
            if(int.Parse(buttons[activeBtn].GetComponent<BtnInfo>().value.GetComponent<TextMeshProUGUI>().text) == ghOb.correctArrangement[index])
            {
                Debug.Log("CORRECT!");
                buttons[activeBtn].GetComponent<Image>().color = Color.green;
                index++;
            }
            else if (int.Parse(buttons[activeBtn].GetComponent<BtnInfo>().value.GetComponent<TextMeshProUGUI>().text) != ghOb.correctArrangement[index])
            {
                Debug.Log("WRONG!");
                buttons[activeBtn].GetComponent<Image>().color = Color.red;
                index++;
            }
            CheckAnswer();
        }
    }

    public void CheckAnswer()
    {
        int correct = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].GetComponent<Image>().color == Color.green)
            {
                correct++;
            }
        }
        if(correct == 5)
        {
            //THE GAME IS DONE!
            player.transform.position = winnerSpawn.transform.position;
            Debug.Log("I am winner!");
        }
    }

    public void ExitingSubmit()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.white;
        }
        index = 0;
        init = false;
    }

    public void EnterSubmit()
    {
        if (!init)
        {
            init = true;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void clearBtn()
    {
        buttons[activeBtn].GetComponent<BtnInfo>().ClearValue();
        canPick = false;
    }
}
