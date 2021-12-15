using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 6000f);
        StartCoroutine(returnPos());
    }
    IEnumerator returnPos() //return snowball to orig position of no player has been hit
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
