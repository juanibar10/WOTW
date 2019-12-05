using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CargarPartida : MonoBehaviour
{

    public void cargarParida()
    {
        FindObjectOfType<ManagerBotones>().FinCargarPartida();
    }
}
