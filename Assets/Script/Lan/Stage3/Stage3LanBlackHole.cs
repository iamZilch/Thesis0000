using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3LanBlackHole : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = GameObject.Find("PlayerPositionHandler3").GetComponent<PositionHandler>().CheckPointGo.transform.position;
        }
    }
}
