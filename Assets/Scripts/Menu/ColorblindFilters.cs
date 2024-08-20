using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public CameraFilter Cam;

    void Start() {
        Cam = Camera.main.GetComponent<CameraFilter>();

        if (PlayerPrefs.GetInt("ToggleBool") == 1) {
            ToggleNone.isOn = true;
        }

        else {
            ToggleNone.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool2") == 1) {
            ToggleProtanopia.isOn = true;
        }

        else {
            ToggleProtanopia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool3") == 1) {
            ToggleProtanomaly.isOn = true;
        }

        else {
            ToggleProtanomaly.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool4") == 1) {
            ToggleDeuteranopia.isOn = true;
        }

        else {
            ToggleDeuteranopia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool5") == 1) {
            ToggleDeuteranomaly.isOn = true;
        }

        else {
            ToggleDeuteranomaly.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool6") == 1) {
            ToggleTritanopia.isOn = true;
        }

        else {
            ToggleTritanopia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool7") == 1) {
            ToggleTritanomaly.isOn = true;
        }

        else {
            ToggleTritanomaly.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool8") == 1) {
            ToggleAchromatopsia.isOn = true;
        }

        else {
            ToggleAchromatopsia.isOn = false;
        }

        if (PlayerPrefs.GetInt("ToggleBool9") == 1) {
            ToggleAchromatomaly.isOn = true;
        }

        else {
            ToggleAchromatomaly.isOn = false;
        }
    }

    void Update() {
        if (ToggleNone.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool", 1);
            Cam.Filter.mode = ColorBlindMode.Normal;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool", 0);
        }

        if (ToggleProtanopia.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool2", 1);
            Cam.Filter.mode = ColorBlindMode.Protanopia;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool2", 0);
        }

        if (ToggleProtanomaly.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool3", 1);
            Cam.Filter.mode = ColorBlindMode.Protanomaly;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool3", 0);
        }

        if (ToggleDeuteranopia.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool4", 1);
            Cam.Filter.mode = ColorBlindMode.Deuteranopia;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool4", 0);
        }

        if (ToggleDeuteranomaly.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool5", 1);
            Cam.Filter.mode = ColorBlindMode.Deuteranomaly;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool5", 0);
        }

        if (ToggleTritanopia.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool6", 1);
            Cam.Filter.mode = ColorBlindMode.Tritanopia;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool6", 0);
        }

        if (ToggleTritanomaly.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool7", 1);
            Cam.Filter.mode = ColorBlindMode.Tritanomaly;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool7", 0);
        }

        if (ToggleAchromatopsia.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool8", 1);
            Cam.Filter.mode = ColorBlindMode.Achromatopsia;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool8", 0);
        }

        if (ToggleAchromatomaly.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool9", 1);
            Cam.Filter.mode = ColorBlindMode.Achromatomaly;
        }

        else {
            PlayerPrefs.SetInt("ToggleBool9", 0);
        }
    }
}
