using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab : MonoBehaviour
{
    [SerializeField] public GameObject[] PlayerPrefabs;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public GameObject getPrefab(int index)
    {
        return PlayerPrefabs[index];
    }
}
