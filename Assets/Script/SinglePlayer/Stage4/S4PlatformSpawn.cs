using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4PlatformSpawn : MonoBehaviour
{
    [SerializeField] public GameObject[] platforms;

    void Start()
    {
        
    }

    public void SpawnPlatforms()
    {
        for(int i = 0; i < platforms.Length; i++)
        {
            for(int y = 0; y < 9; y++)
            {

            }
        }
    }
}
