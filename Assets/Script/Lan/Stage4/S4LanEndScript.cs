using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class S4LanEndScript : NetworkBehaviour
{
    [SerializeField] public GameObject s4;
    [SerializeField] GameObject defaultPos;

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix")))
        {
            if (GameObject.Find("Stage4Handler").GetComponent<LanStage4Handler>().correctAnswer == int.Parse(gameObject.GetComponent<TextMeshPro>().text))
            {
                Debug.Log("Correct! You are right!");
                if (collision.gameObject.GetComponent<PlayerLanExtension>().isLocalPlayer)
                {
                    Debug.Log("I am local player!");
                    GameObject.Find("Stage4Handler").GetComponent<LanStage4Handler>().iAmWinner = true;
                    GameObject.Find("Stage4Handler").GetComponent<LanStage4Handler>().CmdSetWinner(true);
                }
                
            }
            else if(GameObject.Find("Stage4Handler").GetComponent<LanStage4Handler>().correctAnswer != int.Parse(gameObject.GetComponent<TextMeshPro>().text))
            {
                Debug.Log("Wrong! You are left!");
                collision.gameObject.transform.position = defaultPos.transform.position;
            }
        }
    }
}
