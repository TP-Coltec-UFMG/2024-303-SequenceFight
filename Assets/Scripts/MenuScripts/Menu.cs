using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] private GameObject MenuPrincipal;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Modes;
    [SerializeField] private GameObject Keys;

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

    public void closeGame() {
        Debug.Log("Saiu");
        Application.Quit();
    }

    public void infinite() {
        SceneManager.LoadScene("Infinite");
    }

    public void onevsone() {
        SceneManager.LoadScene("1vs1");
    }
}
