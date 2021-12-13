using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerDestroyer : MonoBehaviour
{
    [SerializeField] AudioListener destroy;
    void Update()
    {
        if (GameObject.FindWithTag("Maze") || GameObject.FindWithTag("Trix") || GameObject.FindWithTag("Zilch") || GameObject.FindWithTag("Player"))
            destroy.enabled = false;
        else
            destroy.enabled = true;

    }
}
