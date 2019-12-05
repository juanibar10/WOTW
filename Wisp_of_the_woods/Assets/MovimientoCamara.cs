using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float velocidad;
    private void FixedUpdate()
    {
        print(Input.GetAxis("HorizontalCamara"));
        print(Input.GetAxis("VerticalCamara"));
        //transform.Rotate(transform.eulerAngles.x * Input.GetAxis("HorizontalCamara") * velocidad, transform.eulerAngles.y * Input.GetAxis("VerticalCamara") * velocidad, transform.eulerAngles.y);
    }
}
