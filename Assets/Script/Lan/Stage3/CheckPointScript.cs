using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [Header("Main Script")]
    [SerializeField] GameObject CheckPoint;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            GameObject.Find("PlayerPositionHandler3").GetComponent<PositionHandler>().CheckPointGo = CheckPoint;
        }
    }
}
