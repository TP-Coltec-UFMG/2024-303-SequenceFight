using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBlind : MonoBehaviour {
    public float Player1Health;
    public float Player2Health;
    public int RecordInt = 0;
    public int StreakInt = 0;
    public int LastRecordInt = 0;
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
            LastRecordInt = RecordInt;
        }

        Speak("Bem vindo ao modo cego. Recorde atual " + RecordInt + " .. aperte 1 para repetir, 2 para os detalhes, 3 para ditado lento e 4 para ditado rapido.");

        SelectEnemy();

        Speak("Secoencia . " + CurrentSequence.Replace(" ", PlayerPrefs.GetString("Rate")));

        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        InstantiatePlayer();

        Player1Health = Player1Character.Health;
    }

    void Update() {
        if (!IsPlayerAlive()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                RestartGame();
            }
        }

        if (IsPlayerAlive()) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                StopSpeaking();
                Speak("Secoencia . " + CurrentSequence.Replace(" ", PlayerPrefs.GetString("Rate")));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                StopSpeaking();
                Speak("Vida atual " + Player1Health + ". Poder de ataque " + Player1Character.Damage + " . Inimigo com " + Player2Health + " de vida. E " + Player2Character.Damage + " de poder de ataque.");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                PlayerPrefs.SetString("Rate", "..");
                StopSpeaking();
                Speak("Ditado lento ativado.");
            }

            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                PlayerPrefs.SetString("Rate", ",,,");
                StopSpeaking();
                Speak("Ditado rapido ativado.");
            }
        }
    }

    public void Player1Attack() {
        if (!RestartGameBool) {
            StopSpeaking();

            Player2Health -= Player1Character.Damage;

            Player1Animator.Play("Attack");
            Player2Animator.Play("Hit");

            AudioController.PlayHitSoundEffect("Player1");

            Speak("Correto");

            if (Player2Health <= 0) {
                Speak("Inimigo derrotado.");
                SelectEnemy();

                StreakInt++;

                if (StreakInt >= RecordInt) {
                    RecordInt = StreakInt;
                    PlayerPrefs.SetInt("RecordBlind", StreakInt);
                }
            }
        }
    }

    public void Player2Attack() {
        if (!RestartGameBool) {
            StopSpeaking();

            Player1Health -= Player2Character.Damage;

            Player1Animator.Play("Hit");
            Player2Animator.Play("Attack");

            AudioController.PlayHitSoundEffect("Player2");

            Speak("Incorreto");

            if (Player1Health <= 0) {
                Player1Animator.Play("Die");

                Speak("Voceh morreu. Aperte espaso para recomessar, Esc para sair.");
                
                if (StreakInt > LastRecordInt) {
                    Speak("Novo Recorde " + StreakInt);
                }
                
                ActivateRestartGame();
            }
        }
    }

    public void RestartGame() {
        RestartGameBool = !RestartGameBool;

        StopSpeaking();
        Speak("Recomessando");

        SelectEnemy();

        Speak(CurrentSequence.Replace(" ", PlayerPrefs.GetString("Rate")));

        Player1Health = Player1Character.Health;

        Player1Animator.Play("Idle");

        LastRecordInt = RecordInt;
    }

    public void ActivateRestartGame() {
        RestartGameBool = !RestartGameBool;

        StreakInt = 0;
    }
   
    public void SelectEnemy() {
        if (Player2Instance != null) {
            Destroy(Player2Instance);
        }

        Player2Character = CharacterDB.GetCharacter(GetRandomIndex(0, CharacterDB.CharacterCount - 1));
        
        InstantiateEnemy();

        Speak("Inimigo .. " + Player2Character.CharacterName + ", com " + Player2Character.Health + " de vida e " + Player2Character.Damage + " de poder de ataque. Secoencia ");

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
