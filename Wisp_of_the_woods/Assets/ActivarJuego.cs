using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarJuego : MonoBehaviour
{
    public GameObject Jugador;
    public GameObject Guardias;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Jugador.SetActive(true);
            Guardias.SetActive(true);
            transform.parent.parent.parent.gameObject.SetActive(false);
        }
    }
}
