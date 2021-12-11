using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Blackbox : MonoBehaviour
{
    [Header("Stage 3 Main")]
    [SerializeField] public GameObject main;

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = main.GetComponent<Stage3ScriptHandler>().CheckPointGO.transform.position;
        }
    }
}
