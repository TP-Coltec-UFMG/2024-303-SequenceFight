using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorblindFilter.Scripts;

public class SceneCameraFilter : MonoBehaviour {
    public ColorblindFilter.Scripts.ColorblindFilter Cam;

    void Start() {
        Cam = Camera.main.GetComponent<ColorblindFilter.Scripts.ColorblindFilter>();

        if (Cam != null) {
            if (PlayerPrefs.GetInt("ToggleBool") == -1) {
                Cam.SetUseFilter(false);
            }

            else {
                Cam.SetUseFilter(true);

                if (PlayerPrefs.GetInt("ToggleBool") == 0) {
                    Cam.ChangeBlindType((BlindnessType) 0);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 1) {
                    Cam.ChangeBlindType((BlindnessType) 1);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 2) {
                    Cam.ChangeBlindType((BlindnessType) 2);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 3) {
                    Cam.ChangeBlindType((BlindnessType) 3);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 4) {
                    Cam.ChangeBlindType((BlindnessType) 4);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 5) {
                    Cam.ChangeBlindType((BlindnessType) 5);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 6) {
                    Cam.ChangeBlindType((BlindnessType) 6);
                }

                else if (PlayerPrefs.GetInt("ToggleBool") == 7) {
                    Cam.ChangeBlindType((BlindnessType) 7);
                }
            }
        }
    }
}