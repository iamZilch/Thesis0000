using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectScript : MonoBehaviour
{
    public int Value = 0;
    public float cooldown = 8f;
    public GameObject point;



    // Start is called before the first frame update
    void Start()
    {
        InitColor();
        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitColor()
    {
        Color[] colors = { Color.green, Color.red, Color.blue };
        string[] colorsStr = { "green", "red", "blue" };
        int rand = Random.Range(0, colors.Length);
        switch (rand)
        {
            case 0:
                Value = 1;

                break;
            case 1:
                Value = 2;
                // point.GetComponent<TextMeshPro>().text = 2.ToString();
                break;
            case 2:
                Value = 4;
                // point.GetComponent<TextMeshPro>().text = 4.ToString();
                break;
        }
        var goRenderer = GetComponent<Renderer>();
        goRenderer.material.SetColor("_Color", colors[rand]);
        point.GetComponent<TextMeshPro>().text = Value.ToString();

    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            if (cooldown <= 0)
            {
                InitColor();
                cooldown = Random.Range(8f, 13f);
            }
            cooldown--;
            yield return new WaitForSeconds(1f);
        }
    }
}
