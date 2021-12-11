using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPos = new List<Transform>();
    [SerializeField] GameObject[] powerups;
    [SerializeField] float spawnTime;
    void Start()
    {
        for (int x = 0; x < transform.childCount; x++)
            spawnPos.Add(transform.GetChild(x).GetComponent<Transform>());

        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        bool x = false;
        while (true)
        {
            if (!x)
            {
                x = true;
                yield return new WaitForSeconds(10f);
            }

            else
            {
                GameObject powerUp = Instantiate(powerups[Random.Range(0, 2)], spawnPos[Random.Range(0, transform.childCount)]);
                StartCoroutine(destroyPowerup(powerUp));
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }

    IEnumerator destroyPowerup(GameObject powerup)
    {
        yield return new WaitForSeconds(10f);
        powerup.SetActive(false);
    }
}
