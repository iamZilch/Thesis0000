using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1GoStoreScript : MonoBehaviour
{
    [SerializeField] public GameObject[] TvText;
    [SerializeField] public GameObject[] Platforms;

    private void Start()
    {
        StartCoroutine(StartGameAfter());
    }

    IEnumerator StartGameAfter()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().MasterStart();
        StopAllCoroutines();
    }
}
