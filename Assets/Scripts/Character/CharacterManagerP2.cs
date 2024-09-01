using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManagerP2 : MonoBehaviour {
    public CharacterDatabase CharacterDB;
    public TextMeshProUGUI NameText;
    public SpriteRenderer SpriteArt;
    private int SelectedCharacterP2 = 0;

    void Start() {
        if (!PlayerPrefs.HasKey("SelectedCharacterP2")) {
            SelectedCharacterP2 = 2;
            SaveCharacter();
        }

        else {
            LoadCharacter();
        }

        UpdateCharacter(SelectedCharacterP2);
    }

    public void NextOption() {
        SelectedCharacterP2++;

        if (SelectedCharacterP2 >= CharacterDB.CharacterCount) {
            SelectedCharacterP2 = 0;
        }

        UpdateCharacter(SelectedCharacterP2);
        SaveCharacter();
    }

    private void UpdateCharacter(int SelectedCharacterP2) {
        Character CharacterClone = CharacterDB.GetCharacter(SelectedCharacterP2);
        SpriteArt.sprite = CharacterClone.CharacterSprite;
        NameText.text = CharacterClone.CharacterName;

        if (PlayerPrefs.GetInt("ScreenReader") == 1) {
            UAP_AccessibilityManager.StopSpeaking();
            UAP_AccessibilityManager.Say(CharacterClone.CharacterName + " selecionado.", true, true);
        }
    }

    private void LoadCharacter() {
        SelectedCharacterP2 = PlayerPrefs.GetInt("SelectedCharacterP2");
    }

    private void SaveCharacter() {
        PlayerPrefs.SetInt("SelectedCharacterP2", SelectedCharacterP2);
    }
}
