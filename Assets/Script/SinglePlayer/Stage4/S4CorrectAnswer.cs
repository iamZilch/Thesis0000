using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4CorrectAnswer : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            GameManager.GetComponent<S4GameEngine>().hideUI();
            Debug.Log("Correct! You are right!");
        }
    }
}
