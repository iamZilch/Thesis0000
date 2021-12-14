using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] public GameObject obs;
    public int maxSpawn = 7;
    public int currentSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
        StartCoroutine(ObsChecker());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        //Min x = -5.8 max 54.34
        //max z = 54.32 min = -8.7
        //y = 0.9400001
        for (int i = 0; i < maxSpawn-currentSpawn; i++)
        {
            Instantiate(obs, new Vector3(Random.Range(-5.8f, 54.34f), 1.8f, Random.Range(-8.7f, 54.32f)), Quaternion.identity);
            currentSpawn++;
        }
    }

    IEnumerator ObsChecker()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(Random.Range(4, 6));
        }
    }
}
