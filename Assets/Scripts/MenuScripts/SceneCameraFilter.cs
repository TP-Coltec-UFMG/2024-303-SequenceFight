using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCameraFilter : MonoBehaviour {
    public CameraFilter Cam;

    void Start() {
        Cam = Camera.main.GetComponent<CameraFilter>();

        if (PlayerPrefs.GetInt("ToggleBool") == 1) {
            Cam.Filter.mode = ColorBlindMode.Normal;
        }

        else if (PlayerPrefs.GetInt("ToggleBool2") == 1) {
            Cam.Filter.mode = ColorBlindMode.Protanopia;
        }

        else if (PlayerPrefs.GetInt("ToggleBool3") == 1) {
            Cam.Filter.mode = ColorBlindMode.Protanomaly;
        }

        else if (PlayerPrefs.GetInt("ToggleBool4") == 1) {
            Cam.Filter.mode = ColorBlindMode.Deuteranopia;
        }

        else if (PlayerPrefs.GetInt("ToggleBool5") == 1) {
            Cam.Filter.mode = ColorBlindMode.Deuteranomaly;
        }

        else if (PlayerPrefs.GetInt("ToggleBool6") == 1) {
            Cam.Filter.mode = ColorBlindMode.Tritanopia;
        }

        else if (PlayerPrefs.GetInt("ToggleBool7") == 1) {
            Cam.Filter.mode = ColorBlindMode.Tritanomaly;
        }

        else if (PlayerPrefs.GetInt("ToggleBool8") == 1) {
            Cam.Filter.mode = ColorBlindMode.Achromatopsia;
        }

        else {
            Cam.Filter.mode = ColorBlindMode.Achromatomaly;
        }
    }
}
