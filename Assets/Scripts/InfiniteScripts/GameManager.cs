using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI PlayerHealthUI;
    [SerializeField] private TextMeshProUGUI EnemyHealthUI;
    private float PlayerHealth;
    private float EnemyHealth;
    [SerializeField] private TextMeshProUGUI RecordUI;
    [SerializeField] private TextMeshProUGUI StreakUI;
    private int RecordInt = 0;
    private int StreakInt = 0;
    [SerializeField] private TextMeshProUGUI CurrentSequence;

    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private GameObject EnemyHit;
    [SerializeField] private float HitDuration;

    [SerializeField] private CharacterDatabase CharacterDB;
    private int SelectedCharacterP1;
    private Character Player1Character;
    private Character Player2Character;

    [SerializeField] private TextMeshProUGUI NewRecordText;
    [SerializeField] private GameObject RestartGameUI;
    private bool RestartGameBool = false;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject NewRecordUI;

    [SerializeField] private GameAudioController AudioController;

    public GameObject PlayerInstance;
    public GameObject EnemyInstance;
    public Animator PlayerAnimator;
    public Animator EnemyAnimator;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        InstantiatePlayer();

        PlayerHealth = Player1Character.Health;

        SelectEnemy();

        UpdatePlayerHealth();

        if (!PlayerPrefs.HasKey("Record")) {
            PlayerPrefs.SetInt("Record", RecordInt);
        }

        else {
            RecordInt = PlayerPrefs.GetInt("Record");
        }

        UpdateRecord();

        AudioController.PlayCombatMusic();
    }

    public void PlayerAttack() {
        if (!RestartGameBool) {
            EnemyHealth -= Player1Character.Damage;

            PlayerAnimator.Play("Attack");
            EnemyAnimator.Play("Hit");

            AudioController.PlayHitSoundEffect();
            StartCoroutine(HitIndicator(PlayerHit));

            if (EnemyHealth <= 0) {
                Debug.Log("Player win");
                PlayerHealth += 20;
                SelectEnemy();

                StreakInt++;
                UpdateRecord();
            }

            UpdatePlayerHealth();
        }
    }

    public void EnemyAttack() {
        if (!RestartGameBool) {
            PlayerHealth -= Player2Character.Damage;

            PlayerAnimator.Play("Hit");
            EnemyAnimator.Play("Attack");

            AudioController.PlayHitSoundEffect();
            StartCoroutine(HitIndicator(EnemyHit));

            if (PlayerHealth <= 0) {
                PlayerAnimator.Play("Die");

                Debug.Log("Enemy win");
                ActivateRestartGameUI();
            }

            UpdatePlayerHealth();
        }
    }

    public void UpdatePlayerHealth() {
        PlayerHealthUI.text = "" + PlayerHealth + "";
        EnemyHealthUI.text = "" + EnemyHealth + "";
    }

    public void UpdateRecord() {
        if (StreakInt >= RecordInt) {
            RecordInt = StreakInt;
            PlayerPrefs.SetInt("Record", StreakInt);
        }

        StreakUI.text = "" + StreakInt + "";
        RecordUI.text = "" + RecordInt + "";
    }

    public void UpdateSequence(KeyCode[] sequence) {
        string sequenceString = " ";

        foreach (KeyCode key in sequence) {
            sequenceString += key.ToString() + " ";
        }

        CurrentSequence.text = sequenceString;
    }

    public void RestartGame() {
        AudioController.PlayCombatMusic();

        RestartGameBool = !RestartGameBool;
        GameUI.SetActive(true);
        RestartGameUI.SetActive(false);
        NewRecordUI.SetActive(false);

        SelectEnemy();

        PlayerHealth = Player1Character.Health;
        UpdatePlayerHealth();
        PlayerAnimator.Play("Idle");

        UpdateRecord();
    }

    public void ActivateRestartGameUI() {
        AudioController.PlayYouDiedMusic();

        RestartGameBool = !RestartGameBool;
        GameUI.SetActive(false);
        RestartGameUI.SetActive(true);

        if (StreakInt == RecordInt) {
            NewRecordText.text = RecordUI.text;
            NewRecordUI.SetActive(true);
        }

        StreakInt = 0;
    }
   
    public void SelectEnemy() {
        if (EnemyInstance != null) {
            Destroy(EnemyInstance);
        }

        Player2Character = CharacterDB.GetCharacter(GetRandomIndex(0, CharacterDB.CharacterCount - 1));
        
        InstantiateEnemy();

        EnemyHealth = Player2Character.Health;
    }
    
    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
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
    }

    public void InstantiatePlayer() {
        Vector3 PlayerSpawn = new Vector3(-5.93f, -3.277f, 0);

        PlayerInstance = Instantiate(Player1Character.CharacterPrefab, PlayerSpawn, Quaternion.identity);
        PlayerAnimator = PlayerInstance.GetComponent<Animator>();
    }

    public void InstantiateEnemy() {
        Vector3 EnemySpawn = new Vector3(5.93f, -3.277f, 0);

        EnemyInstance = Instantiate(Player2Character.CharacterPrefab, EnemySpawn, Quaternion.Euler(0, 180, 0));
        EnemyAnimator = EnemyInstance.GetComponent<Animator>();
    }
}
