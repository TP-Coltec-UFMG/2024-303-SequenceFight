using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {   
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name == "Blind" || SceneManager.GetActiveScene().name == "BlindWords")) {
            SceneManager.LoadScene("Menu");
        }
    }
    
    public void Esc() {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
}
