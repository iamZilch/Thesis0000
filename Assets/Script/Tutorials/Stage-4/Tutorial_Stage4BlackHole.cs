using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stage4BlackHole : MonoBehaviour
{
    [Header("Stage 4 Game Engine")]
    [SerializeField] GameObject main;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            other.gameObject.transform.position = main.GetComponent<Tutorial_S4GameEngine>().CheckPointGo.transform.position;
        }
    }
}
