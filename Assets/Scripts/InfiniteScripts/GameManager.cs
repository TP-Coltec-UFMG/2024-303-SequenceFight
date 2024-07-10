using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    private float Player1Health;
    private float Player2Health;
    private int RecordInt = 0;
    private int StreakInt = 0;


    private bool RestartGameBool = false;

    private int SelectedCharacterP1;
    private Character Player1Character;
    private Character Player2Character;
    public GameObject Player1Instance;
    public GameObject Player2Instance;
    public Animator Player1Animator;
    public Animator Player2Animator;
    [SerializeField] private CharacterDatabase CharacterDB;

    public UIController UIManager;
    [SerializeField] private GameAudioController AudioController;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        InstantiatePlayer();

        Player1Health = Player1Character.Health;

        SelectEnemy();

        UIManager.UpdateHealth(Player1Health, Player2Health);

        if (!PlayerPrefs.HasKey("Record")) {
            PlayerPrefs.SetInt("Record", RecordInt);
        }

        else {
            RecordInt = PlayerPrefs.GetInt("Record");
        }

        UIManager.UpdateRecord(StreakInt, RecordInt);

        AudioController.PlayCombatMusic();
    }

    public void Player1Attack() {
        if (!RestartGameBool) {
            Player2Health -= Player1Character.Damage;

            Player1Animator.Play("Attack");
            Player2Animator.Play("Hit");

            AudioController.PlayHitSoundEffect();

            UIManager.ShowPlayerHit();

            if (Player2Health <= 0) {
                Debug.Log("Player win");
                Player1Health += 20;
                SelectEnemy();

                StreakInt++;

                UIManager.UpdateRecord(StreakInt, RecordInt);
            }

            UIManager.UpdateHealth(Player1Health, Player2Health);
        }
    }

    public void Player2Attack() {
        if (!RestartGameBool) {
            Player1Health -= Player2Character.Damage;

            Player1Animator.Play("Hit");
            Player2Animator.Play("Attack");

            AudioController.PlayHitSoundEffect();

            UIManager.ShowEnemyHit();

            if (Player1Health <= 0) {
                Player1Animator.Play("Die");

                Debug.Log("Enemy win");
                ActivateRestartGameUI();
            }

            UIManager.UpdateHealth(Player1Health, Player2Health);
        }
    }

    public void UpdateSequence(KeyCode[] sequence) {
        string sequenceString = " ";

        foreach (KeyCode key in sequence) {
            sequenceString += key.ToString() + " ";
        }

        UIManager.UpdateSequence(sequenceString);
    }

    public void RestartGame() {
        AudioController.PlayCombatMusic();

        RestartGameBool = !RestartGameBool;

        UIManager.RestartGame();

        SelectEnemy();

        Player1Health = Player1Character.Health;

        UIManager.UpdateHealth(Player1Health, Player2Health);

        Player1Animator.Play("Idle");

        UIManager.UpdateRecord(StreakInt, RecordInt);
    }

    public void ActivateRestartGameUI() {
        AudioController.PlayYouDiedMusic();

        RestartGameBool = !RestartGameBool;

        UIManager.ActivateRestartGameUI(StreakInt, RecordInt);

        StreakInt = 0;
    }
   
    public void SelectEnemy() {
        if (Player2Instance != null) {
            Destroy(Player2Instance);
        }

        Player2Character = CharacterDB.GetCharacter(GetRandomIndex(0, CharacterDB.CharacterCount - 1));
        
        InstantiateEnemy();

        Player2Health = Player2Character.Health;
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

        Player1Instance = Instantiate(Player1Character.CharacterPrefab, PlayerSpawn, Quaternion.identity);
        Player1Animator = Player1Instance.GetComponent<Animator>();
    }

    public void InstantiateEnemy() {
        Vector3 EnemySpawn = new Vector3(5.93f, -3.277f, 0);

        Player2Instance = Instantiate(Player2Character.CharacterPrefab, EnemySpawn, Quaternion.Euler(0, 180, 0));
        Player2Animator = Player2Instance.GetComponent<Animator>();
    }
}
