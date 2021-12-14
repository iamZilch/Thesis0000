using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public int Value = 0;
    public float cooldown = 8f;
    

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
                break;
            case 2:
                Value = 4;
                break;
        }
        var goRenderer = GetComponent<Renderer>();
        goRenderer.material.SetColor("_Color", colors[rand]);
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            if(cooldown <= 0)
            {
                InitColor();
                cooldown = Random.Range(8f, 13f);
            }
            cooldown--;
            yield return new WaitForSeconds(1f);
        }
    }
}
