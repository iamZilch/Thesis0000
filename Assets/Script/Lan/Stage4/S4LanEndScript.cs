using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S4LanEndScript : MonoBehaviour
{
    [SerializeField] public GameObject s4;

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            if (s4.GetComponent<S4GameEngine>().correctAnswer == int.Parse(gameObject.GetComponent<TextMeshPro>().text))
            {
                Debug.Log("Correct! You are right!");
            }
            else
            {
                Debug.Log("Wrong! You are left!");
            }
        }
    }
}
