using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4PlatformScript : MonoBehaviour
{
    [SerializeField] public GameObject Value;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch") || collision.gameObject.tag.Equals("Trix"))
        {
            //Find if this shit is wrong path. If yes, set active true.
        }
    }
}
