using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Controller;
using MalbersAnimations.Scriptables;

public class CharacterMovement1 : MonoBehaviour
{
    public CharacterController controller;
    public float velocidad;
    public float gravity;
    public LayerMask mask;
    RaycastHit hit;
    RaycastHit hit2;
    RaycastHit hit3;
    RaycastHit hit4;

    public bool enSuelo;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (!enSuelo)
        {
            input += (Vector3.up * gravity) * Time.deltaTime;
        }

        controller.Move(input * velocidad * Time.deltaTime);
        print(GetAligment());

        Debug.DrawRay(transform.position + new Vector3(0.2f, 0.3f, 0.2f), -transform.up, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.2f, 0.3f, -0.2f), -transform.up, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-0.2f, 0.3f, 0.2f), -transform.up, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-0.2f, 0.3f, -0.2f), -transform.up, Color.red);
    }

    bool GetAligment()
    {
        

        if(Physics.Raycast(transform.position + new Vector3(0.2f, 0.3f, 0.2f)    ,  -transform.up, out hit, 0.7f, mask))
        {
            Physics.Raycast(transform.position + new Vector3(0.2f, 0.3f, -0.2f)  ,  -transform.up, out hit2, 0.7f, mask);
            Physics.Raycast(transform.position + new Vector3(-0.2f, 0.3f, 0.2f)  ,  -transform.up, out hit3, 0.7f, mask);
            Physics.Raycast(transform.position + new Vector3(-0.2f, 0.3f, -0.2f) ,  -transform.up, out hit4, 0.7f, mask);

            Vector3 nextUp = (hit.normal + hit2.normal + hit3.normal + hit4.normal).normalized;

            transform.up = nextUp;
            return true;
        }

        
        return false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            enSuelo = true;
            print("Colisiona con el suelo");
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            enSuelo = false;
            print("No colisiona con el suelo");
        }
    }
}
