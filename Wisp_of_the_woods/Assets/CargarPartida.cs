using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CargarPartida : MonoBehaviour
{

    private void OnLevelWasLoaded(int level)
    {
        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().SetBool("Activo", true);
    }

    public void cargarParida()
    {
        FindObjectOfType<ManagerBotones>().FinCargarPartida();
    }

    public void abc()
    {
        GetComponent<Animator>().SetBool("Activo", false);
    }
   
}
