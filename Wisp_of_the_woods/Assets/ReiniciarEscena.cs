using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarEscena : MonoBehaviour
{
    public bool activo;

    public GameObject Contenedor;
    public GameObject Cinematica;
    public Vector3 posicionInicioJugador;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("a");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        Contenedor = GameObject.FindGameObjectWithTag("Water");
        Cinematica = GameObject.FindGameObjectWithTag("Cinematica");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1 && activo)
        {
            Contenedor = GameObject.FindGameObjectWithTag("Water");
            Cinematica = GameObject.FindGameObjectWithTag("Cinematica");
            Contenedor.transform.GetChild(0).transform.position = posicionInicioJugador;
        }
        else if(level == 0)
        {
            activo = false;
        }

    }

    private void Update()
    {
        if(Contenedor.transform.GetChild(0).gameObject.activeInHierarchy)
            posicionInicioJugador = Contenedor.transform.GetChild(0).GetComponent<characterMovement>().checkpoint;

        if (activo)
        {
            Contenedor.transform.GetChild(0).gameObject.SetActive(true);
            Contenedor.transform.GetChild(1).gameObject.SetActive(true);
            if (Cinematica.activeInHierarchy)
                Cinematica.SetActive(false);
        }
    }

}
