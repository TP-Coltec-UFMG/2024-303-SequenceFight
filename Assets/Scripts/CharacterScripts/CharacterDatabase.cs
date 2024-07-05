using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject {
    public Character[] Characters;
    public int CharacterCount {
        get {
            return Characters.Length;
        }
    }

    public Character GetCharacter(int index) {
        return Characters[index];
    }
}
