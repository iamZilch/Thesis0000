using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stage2BlackHole : MonoBehaviour
{
    [Header("Stage2MainHandler")]
    [SerializeField] GameObject main;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = main.GetComponent<Tutorial_Stage2Manager>().CheckPointGo.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            other.gameObject.transform.position = main.GetComponent<Tutorial_Stage2Manager>().CheckPointGo.transform.position;
        }
    }
}
