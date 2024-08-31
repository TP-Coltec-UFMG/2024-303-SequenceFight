using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class KeyRebindSystem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] TextP1 = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI[] TextP2 = new TextMeshProUGUI[4];

    private KeyCode[] KeyCodesP1 = new KeyCode[4];
    private KeyCode[] KeyCodesP2 = new KeyCode[4];

    private void Start() {
        if (!PlayerPrefs.HasKey("KeyCodeP1_0") || !PlayerPrefs.HasKey("KeyCodeP2_0")) {
            PlayerPrefs.SetInt("KeyCodeP1_0", (int)KeyCode.W);
            PlayerPrefs.SetInt("KeyCodeP1_1", (int)KeyCode.A);
            PlayerPrefs.SetInt("KeyCodeP1_2", (int)KeyCode.S);
            PlayerPrefs.SetInt("KeyCodeP1_3", (int)KeyCode.D);

            PlayerPrefs.SetInt("KeyCodeP2_0", (int)KeyCode.L);
            PlayerPrefs.SetInt("KeyCodeP2_1", (int)KeyCode.K);
            PlayerPrefs.SetInt("KeyCodeP2_2", (int)KeyCode.I);
            PlayerPrefs.SetInt("KeyCodeP2_3", (int)KeyCode.J);
        }

        LoadKeyCodes();
        UpdateTextValues();
    }

    private void UpdateTextValues() {
        for (int i = 0; i < 4; i++) {
            TextP1[i].text = KeyCodesP1[i].ToString();
            TextP2[i].text = KeyCodesP2[i].ToString();
        }
    }

    public void SaveKeyCodes() {
        for (int i = 0; i < KeyCodesP1.Length; i++) {
            PlayerPrefs.SetInt("KeyCodeP1_" + i, (int)KeyCodesP1[i]);
        }

        for (int i = 0; i < KeyCodesP2.Length; i++) {
            PlayerPrefs.SetInt("KeyCodeP2_" + i, (int)KeyCodesP2[i]);
        }
    }

    public void LoadKeyCodes() {
        for (int i = 0; i < KeyCodesP1.Length; i++) {
            KeyCodesP1[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP1_" + i, (int)KeyCode.None);
        }

        for (int i = 0; i < KeyCodesP2.Length; i++) {
            KeyCodesP2[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP2_" + i, (int)KeyCode.None);
        }
    }

    private System.Collections.IEnumerator CaptureKeyPress(int index) {
        GameObject previousSelectedObject = EventSystem.current.currentSelectedGameObject;

        Image ButtonImage = previousSelectedObject.GetComponent<Image>();
        
        Color NewButtomColor = RGBToColor(130, 130, 130);
        Color OldButtomColor = ButtonImage.color;

        ButtonImage.color = NewButtomColor;

        EventSystem.current.SetSelectedGameObject(null);

        yield return null;

        while (!Input.anyKeyDown) {
            yield return null;
        }

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(key)) {
                if (VerifyKey(key)) {
                    Debug.LogError("A tecla " + key + " já foi atribuída.");
                } 
                
                else {
                    if (index < 4) {
                        KeyCodesP1[index] = key;
                        TextP1[index].text = KeyCodesP1[index].ToString();
                    } 
                    
                    else {
                        KeyCodesP2[index - 4] = key;
                        TextP2[index - 4].text = KeyCodesP2[index - 4].ToString();
                    }

                    SaveKeyCodes();
                }

                break;
            }
        }

        ButtonImage.color = OldButtomColor;
        EventSystem.current.SetSelectedGameObject(previousSelectedObject);
    }

    private bool VerifyKey(KeyCode key) {
        foreach (KeyCode k in KeyCodesP1) {
            if (k == key) {
                return true;
            }
        }

        foreach (KeyCode k in KeyCodesP2) {
            if (k == key) {
                return true;
            }
        }

        return false;
    }

    public void Key1P1() {
        StartCoroutine(CaptureKeyPress(0));
    }

    public void Key2P1() {
        StartCoroutine(CaptureKeyPress(1));
    }

    public void Key3P1() {
        StartCoroutine(CaptureKeyPress(2));
    }

    public void Key4P1() {
        StartCoroutine(CaptureKeyPress(3));
    }

    public void Key1P2() {
        StartCoroutine(CaptureKeyPress(4));
    }

    public void Key2P2() {
        StartCoroutine(CaptureKeyPress(5));
    }

    public void Key3P2() {
        StartCoroutine(CaptureKeyPress(6));
    }

    public void Key4P2() {
        StartCoroutine(CaptureKeyPress(7));
    }

    Color RGBToColor(int r, int g, int b, int a = 255) {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
}
