using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManagerBotones : MonoBehaviour
{
    public bool MenuPrincipal;
    public GameObject controles;
    public GameObject titulo;
    public bool activoControles;
    public EventSystem eventSystem;
    public GameObject fundido;

    private void OnLevelWasLoaded(int level)
    {
        if (menuPausa != null)
            menuPausa.SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("Fundido").gameObject);
        
    }

    public GameObject menuPausa;
    private object eventData;

    public void activarGameObject(GameObject objeto)
    {
        objeto.SetActive(!objeto.activeInHierarchy);
    }


    public void cargarPartida()
    {
        GameObject objeto =  Instantiate(fundido, transform);
        objeto.GetComponent<Animator>().Play("FundidoBueno", -1, 0f);
    }

    public void FinCargarPartida()
    {
        StartCoroutine(a());
    }

    public IEnumerator a()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Time.timeScale == 0 && menuPausa != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuPausa.SetActive(true);
        }
        else if(Time.timeScale == 1 && menuPausa != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuPausa.SetActive(false);
        }

        if (activoControles && Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            controles.SetActive(false);
            titulo.SetActive(true);
            eventSystem.gameObject.SetActive(true);
        }

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Resume(characterMovement player)
    {
        StartCoroutine(menupausa(player));
        Time.timeScale = 1;
    }


    public IEnumerator menupausa(characterMovement player)
    {
        yield return new WaitForSeconds(1);
        player.menuPausa = false;
    }
    public void irMenu()
    {
        menuPausa.SetActive(false);
        SceneManager.LoadScene(0);
    }

    
    public void activarControles()
    {
        activoControles = true;
        controles.SetActive(true);
        eventSystem.gameObject.SetActive(false);
    }
}
