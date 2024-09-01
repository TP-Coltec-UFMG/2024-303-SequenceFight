using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class KeyRebindSystem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] TextP1 = new TextMeshProUGUI[5];
    [SerializeField] private TextMeshProUGUI[] TextP2 = new TextMeshProUGUI[5];

    private KeyCode[] KeyCodesP1 = new KeyCode[4];
    private KeyCode[] KeyCodesP2 = new KeyCode[4];

    public KeyCode KeyCodeAbilityP1;
    public KeyCode KeyCodeAbilityP2;

    private void Start() {
        if (!PlayerPrefs.HasKey("KeyCodeP1_0") || !PlayerPrefs.HasKey("KeyCodeP2_0")) {
            PlayerPrefs.SetInt("KeyCodeP1_0", (int)KeyCode.W);
            PlayerPrefs.SetInt("KeyCodeP1_1", (int)KeyCode.A);
            PlayerPrefs.SetInt("KeyCodeP1_2", (int)KeyCode.S);
            PlayerPrefs.SetInt("KeyCodeP1_3", (int)KeyCode.D);
            PlayerPrefs.SetInt("KeyCodeP1_Ability", (int)KeyCode.Q);

            PlayerPrefs.SetInt("KeyCodeP2_0", (int)KeyCode.L);
            PlayerPrefs.SetInt("KeyCodeP2_1", (int)KeyCode.K);
            PlayerPrefs.SetInt("KeyCodeP2_2", (int)KeyCode.I);
            PlayerPrefs.SetInt("KeyCodeP2_3", (int)KeyCode.J);
            PlayerPrefs.SetInt("KeyCodeP2_Ability", (int)KeyCode.U);
        }

        LoadKeyCodes();
        UpdateTextValues();
    }

    private void UpdateTextValues() {
        for (int i = 0; i < 4; i++) {
            TextP1[i].text = KeyCodesP1[i].ToString();
            TextP2[i].text = KeyCodesP2[i].ToString();
        }

        TextP1[4].text = KeyCodeAbilityP1.ToString();
        TextP2[4].text = KeyCodeAbilityP2.ToString();
    }

    public void SaveKeyCodes() {
        for (int i = 0; i < KeyCodesP1.Length; i++) {
            PlayerPrefs.SetInt("KeyCodeP1_" + i, (int)KeyCodesP1[i]);
        }

        for (int i = 0; i < KeyCodesP2.Length; i++) {
            PlayerPrefs.SetInt("KeyCodeP2_" + i, (int)KeyCodesP2[i]);
        }

        PlayerPrefs.SetInt("KeyCodeP1_Ability", (int)KeyCodeAbilityP1);
        PlayerPrefs.SetInt("KeyCodeP2_Ability", (int)KeyCodeAbilityP2);
    }

    public void LoadKeyCodes() {
        for (int i = 0; i < KeyCodesP1.Length; i++) {
            KeyCodesP1[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP1_" + i, (int)KeyCode.None);
        }

        for (int i = 0; i < KeyCodesP2.Length; i++) {
            KeyCodesP2[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP2_" + i, (int)KeyCode.None);
        }

        KeyCodeAbilityP1 = (KeyCode)PlayerPrefs.GetInt("KeyCodeP1_Ability", (int)KeyCode.None);
        KeyCodeAbilityP2 = (KeyCode)PlayerPrefs.GetInt("KeyCodeP2_Ability", (int)KeyCode.None);
    }

    private System.Collections.IEnumerator CaptureKeyPress(int index) {
        GameObject PreviousSelectedObject = EventSystem.current.currentSelectedGameObject;

        Image ButtonImage = PreviousSelectedObject.GetComponent<Image>();
        
        Color NewButtomColor = RGBToColor(130, 130, 130);
        Color OldButtomColor = ButtonImage.color;

        ButtonImage.color = NewButtomColor;

        EventSystem.current.SetSelectedGameObject(null);

        yield return null;

        while (!Input.anyKeyDown) {
            yield return null;
        }

        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(Key)) {
                if (VerifyKey(Key)) {
                    Debug.LogError("A tecla " + Key + " já foi atribuída.");

                    if (PlayerPrefs.GetInt("ScreenReader") == 1) {
                        UAP_AccessibilityManager.StopSpeaking();
                        UAP_AccessibilityManager.Say("A tecla " + Key.ToString() + " jah foi atribuida", true, true);
                    }
                } 
                
                else {
                    if (index < 4) {
                        KeyCodesP1[index] = Key;
                    } 
                    
                    else if (index >= 4 && index < 8) {
                        KeyCodesP2[index - 4] = Key;
                    }

                    else if (index == 8) {
                        KeyCodeAbilityP1 = Key;
                    }

                    else if (index == 9) {
                        KeyCodeAbilityP2 = Key;
                    }

                    if (PlayerPrefs.GetInt("ScreenReader") == 1) {
                        UAP_AccessibilityManager.StopSpeaking();
                        UAP_AccessibilityManager.Say("A tecla " + Key.ToString() + " foi atribuida", true, true);
                    }

                    SaveKeyCodes();
                    UpdateTextValues();
                }

                break;
            }
        }

        ButtonImage.color = OldButtomColor;
        EventSystem.current.SetSelectedGameObject(PreviousSelectedObject);
    }

    private bool VerifyKey(KeyCode Key) {
        foreach (KeyCode K in KeyCodesP1) {
            if (K == Key) {               
                return true;
            }
        }

        foreach (KeyCode K in KeyCodesP2) {
            if (K == Key) {
                return true;
            }
        }

        if (Key == KeyCodeAbilityP1 || Key == KeyCodeAbilityP2) {
            return true;
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

    public void KeyAbilityP1() {
        StartCoroutine(CaptureKeyPress(8));
    }

    public void KeyAbilityP2() {
        StartCoroutine(CaptureKeyPress(9));
    }

    Color RGBToColor(int r, int g, int b, int a = 255) {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
}
