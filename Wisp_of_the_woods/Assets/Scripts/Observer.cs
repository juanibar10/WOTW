using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameObject warning;
    public Transform player;
    public Collider vision;
    bool playerInRange;

    private void Start()
    {
        warning.SetActive(false);
    }

    private void OnTriggerEnter(Collider vision)
    {
        if (vision.transform == player)
            playerInRange = true;

        warning.SetActive(true);
    }
    private void OnTriggerExit(Collider vision)
    {
        if (vision.transform == player)
            playerInRange = false;

        warning.SetActive(false);
    }

    void Update()
    {
     if (playerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                    warning.SetActive(false);
            }

        }
    }

}
