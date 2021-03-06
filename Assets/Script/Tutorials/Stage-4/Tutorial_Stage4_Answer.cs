using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_Stage4_Answer : MonoBehaviour
{
    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject congrats;
    [SerializeField] GameObject displayUI;
    [SerializeField] GameObject buttonHandler;
    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            if (GameManager.GetComponent<Tutorial_S4GameEngine>().correctAnswer == int.Parse(gameObject.GetComponent<TextMeshPro>().text))
            {
                Debug.Log("Correct! You are right!");
                congrats.SetActive(true);
                displayUI.SetActive(false);
                buttonHandler.SetActive(false);
            }
            else
            {
                Debug.Log("Wrong! You are left!");
            }
        }
    }
}
