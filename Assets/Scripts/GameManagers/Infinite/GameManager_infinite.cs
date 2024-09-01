using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerInfinite : MonoBehaviour {
    public float Player1Health;
    public float Player2Health;
    public int RecordInt = 0;
    public int StreakInt = 0;
    public bool RestartGameBool = false;
    public int SelectedCharacterP1;
    public Character Player1Character;
    public Character Player2Character;
    public GameObject Player1Instance;
    public GameObject Player2Instance;
    public Animator Player1Animator;
    public Animator Player2Animator;
    [SerializeField] public CharacterDatabase CharacterDB;
    public UIControllerInfinite UIManager;
    [SerializeField] public GameAudioController AudioController;
    public int EnemyCount = 0;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        InstantiatePlayer();

        Player1Health = Player1Character.Health;

        SelectEnemy();

        UIManager.UpdateHealth(Player1Health, Player2Health);

        if (!PlayerPrefs.HasKey("RecordInfinite")) {
            PlayerPrefs.SetInt("RecordInfinite", RecordInt);
        }

        else {
            RecordInt = PlayerPrefs.GetInt("RecordInfinite");
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
                SelectEnemy();

                StreakInt++;
                EnemyCount++;

                RecordInt = UIManager.UpdateRecord(StreakInt, RecordInt);
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

                ActivateRestartGameUI();
            }

            UIManager.UpdateHealth(Player1Health, Player2Health);
        }
    }

    public void UpdateSequence(KeyCode[] Sequence) {
        string SequenceString = " ";

        foreach (KeyCode key in Sequence) {
            SequenceString += key.ToString() + " ";
        }

        UIManager.UpdateSequence(SequenceString);
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
        EnemyCount = 0;
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
