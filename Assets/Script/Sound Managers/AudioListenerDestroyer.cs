using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerDestroyer : MonoBehaviour
{
    [SerializeField] AudioListener destroy;
    void Update()
    {
        if (GameObject.FindWithTag("Maze").activeInHierarchy || GameObject.FindWithTag("Trix").activeInHierarchy || GameObject.FindWithTag("Zilch").activeInHierarchy || GameObject.FindWithTag("Player").activeInHierarchy)
            destroy.enabled = false;
        else
            destroy.enabled = true;

    }
}
