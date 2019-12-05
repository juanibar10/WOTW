using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class waypoints : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;

    public Transform[] checkpoint;
    public int m_CurrentWaypointIndex;
    private DetectionManager detectionManager;
    public Vector3 posicionOido;
    public GameObject player;
    public bool investigando;
    public int contador;
    public bool rotandoPositivo;
    public float auxRot;

    private float velocidadInvestigar = 20;

    private bool detectado;

    private void Awake()
    {
        player = FindObjectOfType<characterMovement>().gameObject;
        detectionManager = GetComponent<DetectionManager>();
    }

    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(checkpoint[0].position);
    }
    

    void Update()
    {
        

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && !detectionManager.detectadoVisual && !detectionManager.detectadoOido)
        {
            if (m_CurrentWaypointIndex <= checkpoint.Length - 2)
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1);
            else if (m_CurrentWaypointIndex == checkpoint.Length -1 )
                m_CurrentWaypointIndex = 0;

            navMeshAgent.SetDestination(checkpoint[m_CurrentWaypointIndex].transform.position);
        }
        else if(detectionManager.detectadoOido)
        {
            navMeshAgent.SetDestination(posicionOido);
            navMeshAgent.stoppingDistance = 4;

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                investigando = true;
            }
        }

        if (detectionManager.detectadoVisual && !detectado)
        {
            FindObjectOfType<ReiniciarEscena>().activo = true;
            GameObject.FindGameObjectWithTag("Fly").transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ab());

            /*navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.stoppingDistance = 3.5f;
            detectado = true;*/
        }

        /*if(detectionManager.detectadoVisual && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.isStopped = true;
            player.GetComponent<characterMovement>().detectado = true;
            detectado = false;
            StartCoroutine(cambiarLuz());
        }*/

        if (investigando)
        {
            StartCoroutine(Investigar());
        }
    }

    public IEnumerator ab()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    public IEnumerator cambiarLuz()
    {
        yield return new WaitForSeconds(1);
        navMeshAgent.isStopped = false;
        detectionManager.spotLight.color = detectionManager.colorLight;
    }

    public IEnumerator  Investigar()
    {
        yield return new WaitForSeconds(2);
        detectionManager.detectadoOido = false;
        investigando = false;
    }

    public void rotarPositivo()
    {
        transform.Rotate(0, 1 * Time.deltaTime * velocidadInvestigar, 0);
    }
    public void rotarNegativo()
    {
        transform.Rotate(0, -1 * Time.deltaTime * velocidadInvestigar, 0);
    }
    
}
