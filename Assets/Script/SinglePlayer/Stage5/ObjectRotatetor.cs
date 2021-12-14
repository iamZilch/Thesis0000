using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotatetor : MonoBehaviour
{
    private IEnumerator rotate()
    {
        float rotation = 180f;
        while (true)
        {
            rotation += 1;
            transform.rotation = Quaternion.Euler(new Vector3(transform.position.x, rotation, 0)); // rotate on y axis
            yield return new WaitForSeconds(0.01f);
        }

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        StartCoroutine(rotate());
    }
}
