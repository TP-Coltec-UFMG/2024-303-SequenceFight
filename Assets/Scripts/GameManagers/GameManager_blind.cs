using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerBlind : MonoBehaviour {
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

    public UIControllerBlind UIManager;
    [SerializeField] public GameAudioController AudioController;

    public string Label;
    public string Rate = "..";

    void Start() {
        if (!PlayerPrefs.HasKey("RecordBlind")) {
            PlayerPrefs.SetInt("RecordBlind", RecordInt);
        }

        else {
            RecordInt = PlayerPrefs.GetInt("RecordBlind");
        }

        if (!PlayerPrefs.HasKey("Rate")) {
            PlayerPrefs.SetString("Rate", Rate);
        }

        else {
            Rate = PlayerPrefs.GetString("Rate");
        }

        UIManager.UpdateRecord(StreakInt, RecordInt);

        Label = "Bem vindo ao jogo. O Recorde atual eh, " + RecordInt + " .. aperte 1 para leitura lenta, 2 para leitura rapida, e 3 para repetir ";
        UAP_AccessibilityManager.Say(Label, true, true);
        Label = "";

        SelectEnemy();

        UAP_AccessibilityManager.Say("Sequência atual eh .. " + UIManager.GetSequence().Replace(" ", Rate), true, true);

        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        InstantiatePlayer();

        Player1Health = Player1Character.Health;

        UIManager.UpdateHealth(Player1Health, Player2Health);

        AudioController.PlayCombatMusic();
    }

    void Update() {
        if (!IsPlayerAlive()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                RestartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Rate = "..";
            PlayerPrefs.SetString("Rate", Rate);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Rate = ".";
            PlayerPrefs.SetString("Rate", Rate);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            UAP_AccessibilityManager.StopSpeaking();
            UAP_AccessibilityManager.Say("Sequência atual eh .. " + UIManager.GetSequence().Replace(" ", Rate), true, true);
        }
    }

    public void Player1Attack() {
        if (!RestartGameBool) {
            UAP_AccessibilityManager.StopSpeaking();

            Player2Health -= Player1Character.Damage;

            Player1Animator.Play("Attack");
            Player2Animator.Play("Hit");

            AudioController.PlayHitSoundEffect();

            Label = "Sequência Correta";
            UAP_AccessibilityManager.Say(Label, true, true);

            UIManager.ShowPlayerHit();

            if (Player2Health <= 0) {
                Debug.Log("Player win");
                Player1Health += 20;

                Label = "Inimigo derrotado. ";
                SelectEnemy();

                StreakInt++;

                RecordInt = UIManager.UpdateRecord(StreakInt, RecordInt);
            }

            UIManager.UpdateHealth(Player1Health, Player2Health);
        }
    }

    public void Player2Attack() {
        if (!RestartGameBool) {
            UAP_AccessibilityManager.StopSpeaking();

            Player1Health -= Player2Character.Damage;

            Player1Animator.Play("Hit");
            Player2Animator.Play("Attack");

            AudioController.PlayHitSoundEffect();

            Label = "Sequência incorreta";
            UAP_AccessibilityManager.Say(Label, true, true);

            UIManager.ShowEnemyHit();

            if (Player1Health <= 0) {
                Player1Animator.Play("Die");

                Label = "Voceh morreu. Aperte espaso para recomesar, Esc para sair";
                
                if (StreakInt > RecordInt) {
                    Label += " Novo Recorde " + StreakInt;
                }
                
                UAP_AccessibilityManager.Say(Label, true, true);

                Debug.Log("Enemy win");
                ActivateRestartGameUI();
            }

            UIManager.UpdateHealth(Player1Health, Player2Health);
        }
    }

    public void UpdateSequence(KeyCode[] Sequence) {
        string SequenceString = "";

        foreach (KeyCode key in Sequence) {
            SequenceString += key.ToString() + " ";
        }

        UIManager.UpdateSequence(SequenceString);
    }

    public void RestartGame() {
        AudioController.PlayCombatMusic();

        Label = "Recomesando o jogo";
        UAP_AccessibilityManager.Say(Label, true, true);
        Label = "";

        SelectEnemy();

        UAP_AccessibilityManager.Say("Sequência atual eh .. " + UIManager.GetSequence().Replace(" ", Rate), true, true);

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

        Label += "Prosimo inimigo eh o " + Player2Character.CharacterName + ", com " + Player2Character.Health + " de vida.";
        UAP_AccessibilityManager.Say(Label, true, true);

        Player2Health = Player2Character.Health;
    }

    public bool IsPlayerAlive() {
        if (Player1Health <= 0) {
            return false;
        }

        else {
            return true;
        }
    }

    public string GetRate() {
        return Rate;
    }

    public static int GetRandomIndex(int Min, int Max) {
        System.Random random = new System.Random();
        return random.Next(Min, Max + 1);
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
