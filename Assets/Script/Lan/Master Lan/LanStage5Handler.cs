using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanStage5Handler : MonoBehaviour
{
    [SerializeField] public GameObject clearnBtn;
    [SerializeField] public GameObject pickUP;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
