using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    private float PlayerHealth;
    private float EnemyHealth;
    [SerializeField] private TextMeshProUGUI PlayerHealthUI;
    [SerializeField] private TextMeshProUGUI EnemyHealthUI;
    [SerializeField] private TextMeshProUGUI CurrentSequence;
    private AudioSource HitSoundEffect;
    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private GameObject EnemyHit;
    [SerializeField] private float HitDuration;

    public CharacterDatabase CharacterDB;
    public int SelectedCharacterP1;
    public Character Player1Character;
    public Character Player2Character;
    public SpriteRenderer Player1Sprite;
    public SpriteRenderer Player2Sprite;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);
        PlayerHealth = Player1Character.Health;

        SelectEnemy();

        UpdatePlayerHealth();
        HitSoundEffect = FindSoundEffect();
    }

    private AudioSource FindSoundEffect() {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSources) {
            if (source.gameObject.name == "HitSoundEffect") {
                return source;
            }
        }

        Debug.LogWarning("HitSoundEffect AudioSource not found.");
        return null;
    }

    public void PlayerAttack() {
        EnemyHealth -= Player1Character.Damage;
        HitSoundEffect.Play();
        StartCoroutine(HitIndicator(PlayerHit));

        if (EnemyHealth <= 0) {
            Debug.Log("Player win");
            SelectEnemy();
        }

        UpdatePlayerHealth();
    }

    public void EnemyAttack() {
        PlayerHealth -= Player2Character.Damage;
        HitSoundEffect.Play();
        StartCoroutine(HitIndicator(EnemyHit));

        if (PlayerHealth == 0) {
            Debug.Log("Enemy win");
            RestartGame();
        }

        UpdatePlayerHealth();
        
    }

    void UpdatePlayerHealth() {
        PlayerHealthUI.text = "" + PlayerHealth + "";
        EnemyHealthUI.text = "" + EnemyHealth + "";
    }

    public void UpdateSequence(KeyCode[] sequence) {
        string sequenceString = " ";

        foreach (KeyCode key in sequence) {
            sequenceString += key.ToString() + " ";
        }

        CurrentSequence.text = sequenceString;
    }

    void RestartGame() {
        SelectEnemy();

        PlayerHealth = Player1Character.Health;
        EnemyHealth = Player2Character.Health;
    }

    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
    }

    public void SelectEnemy() {
        Player2Character = CharacterDB.GetCharacter(GetRandomIndex(0, CharacterDB.CharacterCount - 1));
        Player2Sprite.sprite = Player2Character.CharacterSprite;
        EnemyHealth = Player2Character.Health;
    }

    public static int GetRandomIndex(int min, int max) {
        System.Random random = new System.Random();
        return random.Next(min, max + 1);
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
        Player1Sprite.sprite = Player1Character.CharacterSprite;
    }

    
}
