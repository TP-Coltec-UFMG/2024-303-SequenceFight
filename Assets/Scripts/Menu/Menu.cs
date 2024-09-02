using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Modes;
    [SerializeField] private GameObject Keys;
    [SerializeField] private GameObject Acessibility;

    [SerializeField] private GameObject MainMenuButton;
    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private GameObject ModesButton;
    [SerializeField] private GameObject KeysButton;
    [SerializeField] private GameObject AcessibilityButton;
    private GameObject LastSelected;

    void Update() {
        if (!Input.GetKey(KeyCode.Mouse0)) {
            LastSelected = EventSystem.current.currentSelectedGameObject;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(LastSelected);
        }
    }

    public void OpenModes() {
        MainMenu.SetActive(false);
        Modes.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ModesButton);
    }

    public void CloseModes() {
        MainMenu.SetActive(true);
        Modes.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MainMenuButton);
    }

    public void OpenSettings() {
        MainMenu.SetActive(false);
        Settings.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsButton);
    }

    public void CloseSettings() {
        MainMenu.SetActive(true);
        Settings.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MainMenuButton);
    }

    public void OpenKeys() {
        Settings.SetActive(false); 
        Keys.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(KeysButton);
    }

    public void CloseKeys() {
        Settings.SetActive(true); 
        Keys.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsButton);
    }

    public void OpenAcessibility() {
        Settings.SetActive(false); 
        Acessibility.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(AcessibilityButton);
    }

    public void CloseAcessibility() {
        Settings.SetActive(true); 
        Acessibility.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SettingsButton);
    }

    public void CloseGame() {
        Application.Quit();
    }

    public void Infinite() {
        SceneManager.LoadScene("Infinite");
    }

    public void Onevsone() {
        SceneManager.LoadScene("1vs1");
    }

    public void Blind() {
        SceneManager.LoadScene("Blind");
    }
    
    public void InfiniteWords() {
        SceneManager.LoadScene("InfiniteWords");
    }

    public void BlindWords() {
        SceneManager.LoadScene("BlindWords");
    }
}