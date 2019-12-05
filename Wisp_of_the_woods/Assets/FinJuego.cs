using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinJuego : MonoBehaviour
{
    public GameObject finJuegoPanel;
    public GameObject Bicho;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            finJuegoPanel.SetActive(true);
        }
    }

    
}
