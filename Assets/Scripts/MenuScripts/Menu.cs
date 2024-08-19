using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] private GameObject PrincipalMenu;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Modes;
    [SerializeField] private GameObject Keys;
    [SerializeField] private GameObject Acessibility;

    [SerializeField] private GameObject PrincipalMenuButtom;
    [SerializeField] private GameObject SettingsButtom;
    [SerializeField] private GameObject ModesButtom;
    [SerializeField] private GameObject KeysButtom;
    [SerializeField] private GameObject AcessibilityButtom;

    public void openModes() {
        PrincipalMenu.SetActive(false);
        Modes.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ModesButtom);
    }

    public void closeModes() {
        PrincipalMenu.SetActive(true);
        Modes.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PrincipalMenuButtom);
    }

    public void openSettings() {
        PrincipalMenu.SetActive(false);
        Settings.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsButtom);
    }

    public void closeSettings() {
        PrincipalMenu.SetActive(true);
        Settings.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PrincipalMenuButtom);
    }

    public void openKeys() {
        Settings.SetActive(false); 
        Keys.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(KeysButtom);
    }

    public void closeKeys() {
        Settings.SetActive(true); 
        Keys.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsButtom);
    }

    public void openAcessibility() {
        Settings.SetActive(false); 
        Acessibility.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(AcessibilityButtom);
    }

    public void closeAcessibility() {
        Settings.SetActive(true); 
        Acessibility.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsButtom);
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
