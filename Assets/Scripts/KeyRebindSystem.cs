using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class KeyRebindSystem : MonoBehaviour {
    public TextMeshProUGUI[] TextP1 = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] TextP2 = new TextMeshProUGUI[4];

    public KeyCode[] KeyCodesP1 = new KeyCode[4] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    public KeyCode[] KeyCodesP2 = new KeyCode[4] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };

    private void Start() {
        LoadKeyCodesFromJson("Assets/Scripts/jsons/keyCodes.json");
        UpdateTextValues();
    }

    private void UpdateTextValues() {
        for (int i = 0; i < 4; i++) {
            TextP1[i].text = KeyCodesP1[i].ToString();
            TextP2[i].text = KeyCodesP2[i].ToString();
        }
    }

    public void SaveKeyCodesToJson(string filePath) {
        KeyCodeData Data = new KeyCodeData();
        Data.KeyCodesP1 = KeyCodesP1;
        Data.KeyCodesP2 = KeyCodesP2;

        string json = JsonUtility.ToJson(Data);

        File.WriteAllText(filePath, json);
    }

    public void LoadKeyCodesFromJson(string filePath) {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);

            KeyCodeData Data = JsonUtility.FromJson<KeyCodeData>(json);

            KeyCodesP1 = Data.KeyCodesP1;
            KeyCodesP2 = Data.KeyCodesP2;
        }
        else {
            Debug.LogError("O arquivo JSON não existe: " + filePath);
        }
    }

    private System.Collections.IEnumerator CaptureKeyPress(int index) {
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
                    } else {
                        KeyCodesP2[index - 4] = key;
                        TextP2[index - 4].text = KeyCodesP2[index - 4].ToString();
                    }

                    SaveKeyCodesToJson("Assets/Scripts/jsons/keyCodes.json");
                }

                break;
            }
        }
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
}
