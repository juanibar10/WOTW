using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public Transform jugador;

    public Vector3 distanciaJugador;
    public float zoomActual;
    public float alturaMirarJugador;
    public float rotacionActualHorizontal;
    public float rotacionActualVertical;
    public float velocidadRotacionHorizontal;
    public float velocidadRotacionVertical;

    public characterMovement characterMovement;
    public float minYAngle = -30;
    public float maxYAngle = 30;
    public Vector2 Sensibility;



    // Update is called once per frame
    void Update()
    {
        if (!characterMovement.menuPausa)
        {
            rotacionActualVertical += Input.GetAxis("Mouse Y") * velocidadRotacionVertical * Time.deltaTime;
            rotacionActualHorizontal += Input.GetAxis("Mouse X") * Sensibility.x * Time.deltaTime;
            rotacionActualVertical = Mathf.Clamp(rotacionActualVertical, minYAngle, maxYAngle);
            print(rotacionActualVertical);
            //distanciaJugador = new Vector3(distanciaJugador.x, rotacionActualVertical, distanciaJugador.z);
            transform.parent.localRotation = Quaternion.Euler(-rotacionActualVertical, rotacionActualHorizontal, transform.parent.localEulerAngles.z);
        }
        
    }
    private void LateUpdate()
    {
       //transform.position = jugador.position - (distanciaJugador * zoomActual); //marcar la distancia entre camara y jugador
       //transform.LookAt(jugador.position + Vector3.up * alturaMirarJugador);// siempre mira al jugador
       //transform.RotateAround(jugador.position, Vector3.up, rotacionActualHorizontal);// siempre rota alrededor del jugador

    }
}
