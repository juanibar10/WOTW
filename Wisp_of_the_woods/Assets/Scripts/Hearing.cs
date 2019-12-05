using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    public GameObject warning;
    public Transform player;
    public KeyCode bark;
    bool m_IsPlayerInRange;
    bool m_isWarned;
    private IEnumerator attention;

    private void Start()
    {
        m_isWarned = false;
        warning.SetActive(m_isWarned);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            m_IsPlayerInRange = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            m_IsPlayerInRange = false;


    }

    private void Update()
    {
        warning.SetActive(m_isWarned);

        if (Input.GetKeyDown(bark) && m_IsPlayerInRange)
            m_isWarned = !m_isWarned;
        else if (!m_IsPlayerInRange)
            StartCoroutine(mantener());
    }


    IEnumerator mantener()
    {
        yield return new WaitForSeconds(5);
        m_isWarned = !m_isWarned;
    }
}
