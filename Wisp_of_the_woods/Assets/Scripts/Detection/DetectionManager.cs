using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class DetectionManager : MonoBehaviour
{
    public bool detectadoVisual;
    public bool detectadoOido;
    public bool detectable;

    public  Light spotLight;
    public Color colorLight;
    public GameObject player;
    private LookatTarget lookatTarget;


    private void Awake()
    {
        player = FindObjectOfType<characterMovement>().gameObject;
        lookatTarget = GetComponent<LookatTarget>();
    }

    private void Update()
    {
        if (detectadoVisual)
        {
            spotLight.color = Color.red;
            lookatTarget.enabled = true;
        }
        else
        {
            lookatTarget.enabled = false;
        }
    }

    
    

}
