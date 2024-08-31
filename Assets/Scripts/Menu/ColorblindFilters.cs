using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ColorblindFilter.Scripts;

public class ColorblindFilters : MonoBehaviour {
    public Toggle ToggleNone;
    public Toggle ToggleProtanopia;
    public Toggle ToggleProtanomaly;
    public Toggle ToggleDeuteranopia;
    public Toggle ToggleDeuteranomaly;
    public Toggle ToggleTritanopia;
    public Toggle ToggleTritanomaly;
    public Toggle ToggleAchromatopsia;
    public Toggle ToggleAchromatomaly;
    public ColorblindFilter.Scripts.ColorblindFilter Cam;

    void Start() {
        if(!PlayerPrefs.HasKey("ToggleBool")) {
            PlayerPrefs.SetInt("ToggleBool", -1);
        }

        Cam = Camera.main.GetComponent<ColorblindFilter.Scripts.ColorblindFilter>();

        if (PlayerPrefs.GetInt("ToggleBool") == -1) {
            ToggleNone.isOn = true;
        }

        else {
            ToggleNone.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 0) {
            ToggleProtanopia.isOn = true;
        }

        else {
            ToggleProtanopia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 1) {
            ToggleProtanomaly.isOn = true;
        }

        else {
            ToggleProtanomaly.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 2) {
            ToggleDeuteranopia.isOn = true;
        }

        else {
            ToggleDeuteranopia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 3) {
            ToggleDeuteranomaly.isOn = true;
        }

        else {
            ToggleDeuteranomaly.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 4) {
            ToggleTritanopia.isOn = true;
        }

        else {
            ToggleTritanopia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 5) {
            ToggleTritanomaly.isOn = true;
        }

        else {
            ToggleTritanomaly.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 6) {
            ToggleAchromatopsia.isOn = true;
        }

        else {
            ToggleAchromatopsia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool") == 7) {
            ToggleAchromatomaly.isOn = true;
        }

        else {
            ToggleAchromatomaly.isOn = false;
        }
    }

    void Update() {
        if (ToggleNone.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool", -1);
            Cam.SetUseFilter(false);
        }

        else {
            Cam.SetUseFilter(true);

            if (ToggleProtanopia.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 0);
                Cam.ChangeBlindType((BlindnessType) 0);
            }

            if (ToggleProtanomaly.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 1);
                Cam.ChangeBlindType((BlindnessType) 1);

            }

            if (ToggleDeuteranopia.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 2);
                Cam.ChangeBlindType((BlindnessType) 2);
            }

            if (ToggleDeuteranomaly.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 3);
                Cam.ChangeBlindType((BlindnessType) 3);
            }

            if (ToggleTritanopia.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 4);
                Cam.ChangeBlindType((BlindnessType) 4);
            }

            if (ToggleTritanomaly.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 5);
                Cam.ChangeBlindType((BlindnessType) 5);
            }

            if (ToggleAchromatopsia.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 6);
                Cam.ChangeBlindType((BlindnessType) 6);
            }

            if (ToggleAchromatomaly.isOn == true) {
                PlayerPrefs.SetInt("ToggleBool", 7);
                Cam.ChangeBlindType((BlindnessType) 7);
            }
        }        
    }
}
