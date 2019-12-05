using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Controller;
using MalbersAnimations.Scriptables;

public class CharacterMovement1 : MonoBehaviour
{

    public float velocidad;
    
    void FixedUpdate()
    {
        

        if (/*Input.GetAxis("Horizontal") == 1 || */ Input.GetAxis("Vertical") == 1 )
            GetComponent<Animator>().SetFloat("Vertical", 1);
        else if(/*Input.GetAxis("Horizontal") == -1 || */Input.GetAxis("Vertical") == -1)
            GetComponent<Animator>().SetFloat("Vertical", -1);
        else
        {
            //GetComponent<Animator>().SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            GetComponent<Animator>().SetFloat("Vertical", Input.GetAxis("Vertical"));
        }
        
        if(Input.GetAxis("Vertical") > 0)
        {
            GetComponent<MAnimal>().alwaysForward.Value = true;
        }
        else
        {
            GetComponent<MAnimal>().alwaysForward.Value = false;
        }

        /* if(Input.GetAxis("Horizontal") > 0)
             transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * velocidad * Time.fixedDeltaTime, Space.Self);
         else if(Input.GetAxis("Horizontal") < 0)
             transform.Translate(Vector3.back * Input.GetAxis("Horizontal") * velocidad * Time.fixedDeltaTime, Space.Self);
         else */
        if (Input.GetAxis("Vertical") > 0)
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * velocidad * Time.fixedDeltaTime, Space.Self);
        else if (Input.GetAxis("Vertical") < 0)
            transform.Translate(Vector3.back * Input.GetAxis("Vertical") * velocidad * Time.fixedDeltaTime, Space.Self);
            
    }
}
