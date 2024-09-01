using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenReaderManager : MonoBehaviour {
    private GameObject LastSelectedObject;

    private void Start() {
        if (!PlayerPrefs.HasKey("ScreenReader")) {
            PlayerPrefs.SetInt("ScreenReader", 0);
        }
    }

    private void Update() {
        if (PlayerPrefs.GetInt("ScreenReader") == 1) {
            GameObject CurrentSelectedObject = EventSystem.current.currentSelectedGameObject;

            if (CurrentSelectedObject != null && CurrentSelectedObject != LastSelectedObject) {
                LastSelectedObject = CurrentSelectedObject;

                OnObjectSelected(CurrentSelectedObject);
            }
        }
    }

    public void OnObjectSelected(GameObject SelectedObject) {
        Label GameObjectLabel = SelectedObject.GetComponent<Label>();

        string TextToRead = GameObjectLabel.GetOnSelectionLabel();

        if (!string.IsNullOrEmpty(TextToRead)) {
            UAP_AccessibilityManager.StopSpeaking();
            UAP_AccessibilityManager.Say(TextToRead, true, true);
        }

        ConfigureClickListener(SelectedObject);
    }

    private void ConfigureClickListener(GameObject SelectedObject) {
        Button SelectedButton = SelectedObject.GetComponent<Button>();

        if (SelectedButton != null) {
            SelectedButton.onClick.RemoveAllListeners();

            SelectedButton.onClick.AddListener(() => {
                if (UAP_AccessibilityManager.IsSpeaking()) {
                    UAP_AccessibilityManager.StopSpeaking();
                }
                
                OnObjectClicked(SelectedObject);
            });
        }

        Toggle SelectedToggle = SelectedObject.GetComponent<Toggle>();

        if (SelectedToggle != null) {
            SelectedToggle.onValueChanged.RemoveAllListeners();

            SelectedToggle.onValueChanged.AddListener(isOn => {
                if (isOn) {
                    UAP_AccessibilityManager.StopSpeaking();
                    OnObjectClicked(SelectedObject);
                }
            });
        }
    }

    public void OnObjectClicked(GameObject clickedObject) {
        Label GameObjectLabel = clickedObject.GetComponent<Label>();

        string textToRead = GameObjectLabel.GetOnClickLabel();

        if (!string.IsNullOrEmpty(textToRead)) {
            UAP_AccessibilityManager.Say(textToRead, false, true);
        }
    }
}
