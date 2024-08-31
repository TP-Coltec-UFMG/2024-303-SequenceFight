using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMouse : MonoBehaviour {
    void Start() {
        UAP_AccessibilityManager.Say("Ola", true, true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
