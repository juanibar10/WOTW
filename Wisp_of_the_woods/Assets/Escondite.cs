using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escondite : MonoBehaviour
{
    public Color Normal;
    public Color Escondido;
    public Material material;

    private void Awake()
    {
        material.SetColor("_colorBase", Normal);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            material.SetColor("_colorBase", Escondido);
            other.GetComponent<characterMovement>().escondido = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            material.SetColor("_colorBase", Normal);
            other.GetComponent<characterMovement>().escondido = false;
        }
    }
}
