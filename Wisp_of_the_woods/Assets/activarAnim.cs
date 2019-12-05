using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarAnim : MonoBehaviour
{
    public void ActivarAnim()
    {
        GetComponent<Animator>().SetBool("Entra", true);
    }

    public void DesactivarAnim()
    {
        GetComponent<Animator>().SetBool("Entra", false);
    }

    public void GameObjectFalse()
    {
        gameObject.SetActive(false);
    }
}
