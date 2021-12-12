using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject main;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            main.GetComponent<S4GameEngine>().setCheckpoint(1);
        }
    }
}
