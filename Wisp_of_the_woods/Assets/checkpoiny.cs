using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoiny : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<characterMovement>().checkpoint = other.transform.position;
        }
    }
}
