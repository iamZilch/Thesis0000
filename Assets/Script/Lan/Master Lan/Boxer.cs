using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{
    [SerializeField] public GameObject posHandler;

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = posHandler.GetComponent<PositionHandler>().CheckPointGo.transform.position;
        }
    }
}
