using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSound : MonoBehaviour
{
    private DetectionManager detectionManager;


    private void Awake()
    {
        detectionManager = transform.parent.parent.gameObject.GetComponent<DetectionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            detectionManager.detectable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectionManager.detectable = false;
        }
    }
}
