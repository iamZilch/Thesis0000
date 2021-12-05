using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject spawner;
    void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);
        StartCoroutine(returnPos());
    }

    void OnDisable()
    {
        gameObject.transform.position = spawner.transform.position;
        gameObject.transform.rotation = spawner.transform.rotation;
    }

    IEnumerator returnPos() //return snowball to orig position of no player has been hit
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
