using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4PlatformSpawn : MonoBehaviour
{
    [SerializeField] public GameObject[] platforms;

    void Start()
    {
        SpawnPlatforms();
    }

    public void SpawnPlatforms()
    {
        float zPos = 56.4f;
        for(int i = 0; i < platforms.Length; i++)
        {
            float xPos = 14.6f;
            //Reset X to -123.8
            for(int y = 0; y < 9; y++)
            {
                //Debug.Log($"Outer : {i} --- inner : {y} ---- Value: {xPos}");
                //X + 14.6
                Instantiate(platforms[i], new Vector3(xPos, 0, zPos), Quaternion.identity);
                xPos += 14.6f;
            }
            //Z + 14.6
            zPos -= 14.6f;
        }
    }
}
