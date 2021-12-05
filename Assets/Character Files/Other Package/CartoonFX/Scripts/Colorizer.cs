using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class Colorizer : MonoBehaviour
{

    public Color TintColor;
    public bool UseInstanceWhenNotEditorMode = true;

    private Color oldColor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (oldColor != TintColor) ChangeColor(gameObject, TintColor);
        oldColor = TintColor;
    }

    void ChangeColor(GameObject effect, Color color)
    {
        Material mat;
        var rend = effect.GetComponentsInChildren<Renderer>();
        foreach (var r in rend)
        {


            mat = r.sharedMaterial;

            if (UseInstanceWhenNotEditorMode) mat = r.material;
            else mat = r.sharedMaterial;


            if (mat == null || !mat.HasProperty("_TintColor")) continue;
            var oldColor = mat.GetColor("_TintColor");
            color.a = oldColor.a;
            mat.SetColor("_TintColor", color);
        }
        var light = effect.GetComponentInChildren<Light>();
        if (light != null) light.color = color;
    }
}
