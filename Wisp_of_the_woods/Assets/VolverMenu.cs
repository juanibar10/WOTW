using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    public bool activo;

    public GameObject bicho;
    public void activarFinJuego()
    {
        bicho.SetActive(false);
        activo = true;
    }

    private void Update()
    {
        if (activo && Input.anyKeyDown)
            SceneManager.LoadScene(0);
    }
}
