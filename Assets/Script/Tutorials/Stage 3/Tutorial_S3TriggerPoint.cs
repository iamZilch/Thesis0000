using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_S3TriggerPoint : MonoBehaviour
{
    [Header("Main Script")]
    [SerializeField] GameObject Stage3Main;

    [Header("Main Script")]
    [SerializeField] GameObject CheckPoint;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            Stage3Main.GetComponent<Tutorial_Stage3_MainHandler>().CheckPointGO = CheckPoint;
        }
    }
}
