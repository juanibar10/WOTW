using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion : MonoBehaviour
{
    private Animator anim;
    private characterMovement characterMovement;

    private void Awake()
    {
        characterMovement = FindObjectOfType<characterMovement>();
    }

    public void cambiarPosicion()
    {
        characterMovement.transform.position = characterMovement.checkpoint;
        characterMovement.ResetearEnemigos();
        characterMovement.detectado = false;
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
