using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour
{

    public FixedTouchField touchField;
    [SerializeField] GameObject cam;
    protected float CameraAngle;
    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CameraAngle += touchField.TouchDist.x * CameraAngleSpeed;
        CameraAngleY += touchField.TouchDist.y * CameraAngleSpeed;

        cam.transform.rotation = Quaternion.Euler(CameraAngleY, CameraAngle, 0.0f);

        // cam.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3(0, 3, 4);
        // cam.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);

    }
}
