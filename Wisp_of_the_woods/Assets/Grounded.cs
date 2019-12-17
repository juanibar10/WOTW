using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Suelo")
            GetComponentInParent<CharacterMovement1>().enSuelo = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Suelo")
            GetComponentInParent<CharacterMovement1>().enSuelo = false;
    }
}
