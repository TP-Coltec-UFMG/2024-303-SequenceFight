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
    [SerializeField] public GameAudioController AudioController;
    public string CurrentSequence;

    void Start() {
        if (!PlayerPrefs.HasKey("RecordBlind")) {
            PlayerPrefs.SetInt("RecordBlind", RecordInt);
        }

        else {
            RecordInt = PlayerPrefs.GetInt("RecordBlind");
        }

        PlayerPrefs.SetString("Rate", "..");

        Speak("Bem vindo ao modo blayindi. O Recorde atual eh, " + RecordInt + " .. aperte 1 para ditado lento, 2 para ditado rapido, 3 para repetir e 4 para os detalhes.");

        SelectEnemy();

        Speak("Secoencia atual . " + CurrentSequence.Replace(" ", PlayerPrefs.GetString("Rate")));

        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        InstantiatePlayer();

        Player1Health = Player1Character.Health;

        AudioController.PlayCombatMusic();
    }

    void Update() {
        if (!IsPlayerAlive()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                RestartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            PlayerPrefs.SetString("Rate", "..");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            PlayerPrefs.SetString("Rate", ",,,");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            StopSpeaking();
            Speak("Secoencia atual . " + CurrentSequence.Replace(" ", PlayerPrefs.GetString("Rate")));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            StopSpeaking();
            Speak("Vida atual " + Player1Health + ". Voceh tem " + Player1Character.Damage + " de poder de ataque. Inimigo estah com " + Player2Health + " de vida. E possui " + Player2Character.Damage + " de poder de ataque.");
        }
    }

    public void Player1Attack() {
        if (!RestartGameBool) {
            StopSpeaking();

            Player2Health -= Player1Character.Damage;

            Player1Animator.Play("Attack");
            Player2Animator.Play("Hit");

            AudioController.PlayHitSoundEffect();

            Speak("Secoencia Correta");

            if (Player2Health <= 0) {
                Speak("Inimigo derrotado.");
                SelectEnemy();

                StreakInt++;

                if (StreakInt >= RecordInt) {
                    RecordInt = StreakInt;
                    PlayerPrefs.SetInt("RecordBlind", StreakInt);
                }

                RecordInt = StreakInt;
            }
        }
    }

    public void Player2Attack() {
        if (!RestartGameBool) {
            StopSpeaking();

            Player1Health -= Player2Character.Damage;

            Player1Animator.Play("Hit");
            Player2Animator.Play("Attack");

            AudioController.PlayHitSoundEffect();

            Speak("Secoencia incorreta");

            if (Player1Health <= 0) {
                Player1Animator.Play("Die");

                Speak("Voceh morreu. Aperte espaso para recomessar, Esc para sair.");
                
                if (StreakInt == RecordInt) {
                    Speak("Novo Recorde " + StreakInt);
                }
                
                ActivateRestartGameUI();
            }
        }
    }

    public void RestartGame() {
        AudioController.PlayCombatMusic();

        RestartGameBool = !RestartGameBool;

        StopSpeaking();
        Speak("Recomessando o jogo");

        SelectEnemy();

        Speak("Secoencia atual . " + CurrentSequence.Replace(" ", PlayerPrefs.GetString("Rate")));

        Player1Health = Player1Character.Health;

        Player1Animator.Play("Idle");
    }

    public void ActivateRestartGameUI() {
        AudioController.PlayYouDiedMusic();

        RestartGameBool = !RestartGameBool;

        StreakInt = 0;
    }
   
    public void SelectEnemy() {
        if (Player2Instance != null) {
            Destroy(Player2Instance);
        }

        Player2Character = CharacterDB.GetCharacter(GetRandomIndex(0, CharacterDB.CharacterCount - 1));
        
        InstantiateEnemy();

        Speak("Inimigo Atual .. " + Player2Character.CharacterName + ", com " + Player2Character.Health + " de vida.");

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

    public void Speak(string Label) {
        UAP_AccessibilityManager.Say(Label, true, true);
    }

    public void StopSpeaking() {
        UAP_AccessibilityManager.StopSpeaking();
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
