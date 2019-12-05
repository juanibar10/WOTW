using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charcontroller : MonoBehaviour
{
    public float vel;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(cam.transform.forward * Input.GetAxis("Vertical") * vel * Time.deltaTime);
        transform.Translate(cam.transform.right * Input.GetAxis("Horizontal") * vel * Time.deltaTime);

    }
}
