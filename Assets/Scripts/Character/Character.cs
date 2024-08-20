using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
    public string CharacterName;
    public Sprite CharacterSprite;
    public int SequenceLength;
    public float Health;
    public float Damage;
    public GameObject CharacterPrefab;
}
