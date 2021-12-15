using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stage3_BlackHole : MonoBehaviour
{
    [Header("Stage 3 Main")]
    [SerializeField] GameObject main;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = main.GetComponent<Tutorial_Stage3_MainHandler>().CheckPointGO.transform.position;
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = main.GetComponent<Tutorial_Stage3_MainHandler>().CheckPointGO.transform.position;
        }
    }

    
}
