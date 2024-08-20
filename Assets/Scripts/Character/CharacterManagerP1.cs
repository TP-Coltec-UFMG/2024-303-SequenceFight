using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManagerP1 : MonoBehaviour {
    public CharacterDatabase CharacterDB;
    public TextMeshProUGUI NameText;
    public SpriteRenderer SpriteArt;
    private int SelectedCharacterP1 = 0;

    void Start() {
        if (!PlayerPrefs.HasKey("SelectedCharacterP1")) {
            SelectedCharacterP1 = 0;
            SaveCharacter();
        }

        else {
            LoadCharacter();
        }

        UpdateCharacter(SelectedCharacterP1);
    }

    public void NextOption() {
        SelectedCharacterP1++;

        if (SelectedCharacterP1 >= CharacterDB.CharacterCount) {
            SelectedCharacterP1 = 0;
        }

        UpdateCharacter(SelectedCharacterP1);
        SaveCharacter();
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Character CharacterClone = CharacterDB.GetCharacter(SelectedCharacterP1);
        SpriteArt.sprite = CharacterClone.CharacterSprite;
        NameText.text = CharacterClone.CharacterName;
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void SaveCharacter() {
        PlayerPrefs.SetInt("SelectedCharacterP1", SelectedCharacterP1);
    }
}
