using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenReaderToggle : MonoBehaviour {
    [SerializeField] public Toggle ScreenReader;
    void Start() {
        if (PlayerPrefs.GetInt("ScreenReader") == 1) {
            ScreenReader.isOn = true;
        }

        else {
            ScreenReader.isOn = false;
        }
    }

    void Update() {
        if (ScreenReader.isOn == true) {
            PlayerPrefs.SetInt("ScreenReader", 1);
        }

        else {
            PlayerPrefs.SetInt("ScreenReader", 0);
        }
    }
}
