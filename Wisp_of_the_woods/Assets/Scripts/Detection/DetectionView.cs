using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionView : MonoBehaviour
{

    public DetectionManager detectionManager;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<characterMovement>().escondido)
                detectionManager.detectadoVisual = true;
        }
    }
}
