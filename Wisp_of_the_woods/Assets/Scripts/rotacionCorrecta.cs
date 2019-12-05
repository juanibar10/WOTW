using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionCorrecta : MonoBehaviour
{
    public Transform Camara;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(Camara.rotation.eulerAngles.x - (Camara.rotation.eulerAngles.x * 2), 0, Camara.rotation.z - (Camara.rotation.z * 2));
    }
}
