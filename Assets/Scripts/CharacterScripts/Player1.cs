using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {
    public CharacterDatabase CharacterDB;
    public SpriteRenderer SpriteArt;
    private int SelectedCharacterP1 = 0;

    void Start() {
        if (!PlayerPrefs.HasKey("SelectedCharacterP1")) {
            SelectedCharacterP1 = 0;
        }

        else {
            LoadCharacter();
        }

        UpdateCharacter(SelectedCharacterP1);
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Character CharacterClone = CharacterDB.GetCharacter(SelectedCharacterP1);
        SpriteArt.sprite = CharacterClone.CharacterSprite;
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }
}
