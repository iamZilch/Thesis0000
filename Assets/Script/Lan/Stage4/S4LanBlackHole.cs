using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4LanBlackHole : MonoBehaviour
{
    [SerializeField] GameObject deadSpawnPoint;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = deadSpawnPoint.transform.position;
        }
    }
}
