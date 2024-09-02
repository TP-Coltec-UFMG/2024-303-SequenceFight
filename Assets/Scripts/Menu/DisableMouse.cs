using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableMouse : MonoBehaviour {
    private GameObject LastSelected;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (!Input.GetKey(KeyCode.Mouse0)) {
            LastSelected = EventSystem.current.currentSelectedGameObject;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(LastSelected);
        }
    }
}
