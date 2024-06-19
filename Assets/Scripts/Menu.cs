using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] private GameObject MenuPrincipal;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Modes;
    [SerializeField] private GameObject Keys;
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    public void openModes() {
        MenuPrincipal.SetActive(false);
        Modes.SetActive(true);
    }

    public void closeModes() {
        MenuPrincipal.SetActive(true);
        Modes.SetActive(false);
    }

    public void openSettings() {
        MenuPrincipal.SetActive(false);
        Settings.SetActive(true);
    }

    public void closeSettings() {
        MenuPrincipal.SetActive(true);
        Settings.SetActive(false);
    }

    public void openKeys() {
       Settings.SetActive(false); 
       Keys.SetActive(true);
    }

    public void closeKeys() {
       Settings.SetActive(true); 
       Keys.SetActive(false);
    }

    public void openP1() {
       MenuPrincipal.SetActive(false); 
       P1.SetActive(true);
    }

    public void closeP1() {
       MenuPrincipal.SetActive(true); 
       P1.SetActive(false);
    }

    public void openP2() {
       MenuPrincipal.SetActive(false); 
       P2.SetActive(true);
    }

    public void closeP2() {
       MenuPrincipal.SetActive(true); 
       P2.SetActive(false);
    }

    public void closeGame() {
        Debug.Log("Saiu");
        Application.Quit();
    }

    public void infinite() {
        SceneManager.LoadScene("Jogo");
    }
}
