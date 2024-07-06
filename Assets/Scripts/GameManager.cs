using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    private float PlayerHealth;
    private float EnemyHealth = 100f;
    [SerializeField] private TextMeshProUGUI PlayerHealthUI;
    [SerializeField] private TextMeshProUGUI EnemyHealthUI;
    [SerializeField] private TextMeshProUGUI CurrentSequence;
    private AudioSource HitSoundEffect;
    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private GameObject EnemyHit;
    [SerializeField] private float HitDuration;

    public int SelectedCharacterP1;
    public CharacterDatabase CharacterDB;
    public Character Player1Character;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);
        PlayerHealth = Player1Character.Health;

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

        if (EnemyHealth == 0) {
            Debug.Log("Player win");
            RestartGame();
        }

        else {
            UpdatePlayerHealth();
        }
    }

    public void EnemyAttack() {
        PlayerHealth -= 10f;
        HitSoundEffect.Play();
        StartCoroutine(HitIndicator(EnemyHit));

        if (PlayerHealth == 0) {
            Debug.Log("Enemy win");
            RestartGame();
        }

        else {
            UpdatePlayerHealth();
        }
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
        PlayerHealth = Player1Character.Health;
        EnemyHealth = 100f;

        UpdatePlayerHealth();
    }

    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
    }
}
