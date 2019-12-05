using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacioncam : MonoBehaviour
{
    public float vel;

    float roty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    { 

        roty += Input.GetAxis("Mouse X") * vel * Time.deltaTime;

        transform.rotation = Quaternion.Euler(transform.rotation.x, roty,transform.rotation.z);
    }
}
