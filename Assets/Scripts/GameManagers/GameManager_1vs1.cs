using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager1vs1 : MonoBehaviour {
    private float Player1Health;
    private float Player2Health;

    private bool RestartGameBool = false;

    private int SelectedCharacterP1;
    private int SelectedCharacterP2;
    private Character Player1Character;
    private Character Player2Character;
    public GameObject Player1Instance;
    public GameObject Player2Instance;
    public Animator Player1Animator;
    public Animator Player2Animator;
    [SerializeField] private CharacterDatabase CharacterDB;

    public UIController1vs1 UIManager;
    public bool P1winBool = false;
    public bool P2winBool = false;
    [SerializeField] private GameAudioController AudioController;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1, SelectedCharacterP2);

        InstantiatePlayer1();
        InstantiatePlayer2();

        Player1Health = Player1Character.Health;
        Player2Health = Player2Character.Health;

        UIManager.UpdateHealth(Player1Health, Player2Health);

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
                P1winBool = true;

                Player2Animator.Play("Die");

                Debug.Log("Player1 win");
                ActivateRestartGameUI();
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

            UIManager.ShowPlayerHit();

            if (Player1Health <= 0) {
                P2winBool = true;

                Player1Animator.Play("Die");

                Debug.Log("Player2 win");
                ActivateRestartGameUI();
            }

            UIManager.UpdateHealth(Player1Health, Player2Health);
        }
    }

    public void UpdateSequenceP1(KeyCode[] sequence) {
        string sequenceString = " ";

        foreach (KeyCode key in sequence) {
            sequenceString += key.ToString() + " ";
        }

        UIManager.UpdateSequenceP1(sequenceString);
    }

    public void UpdateSequenceP2(KeyCode[] sequence) {
        string sequenceString = " ";

        foreach (KeyCode key in sequence) {
            sequenceString += key.ToString() + " ";
        }

        UIManager.UpdateSequenceP2(sequenceString);
    }

    public void RestartGame() {
        AudioController.PlayCombatMusic();

        RestartGameBool = !RestartGameBool;

        P1winBool = false;
        P2winBool = false;

        UIManager.RestartGame();

        Player1Health = Player1Character.Health;
        Player2Health = Player2Character.Health;

        UIManager.UpdateHealth(Player1Health, Player2Health);

        Player1Animator.Play("Idle");
        Player2Animator.Play("Idle");
    }

    public void ActivateRestartGameUI() {
        AudioController.PlayYouDiedMusic();

        UIManager.ActivateRestartGameUI(P1winBool, P2winBool);

        RestartGameBool = !RestartGameBool;
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
        SelectedCharacterP2 = PlayerPrefs.GetInt("SelectedCharacterP2");
    }

    private void UpdateCharacter(int SelectedCharacterP1, int SelectedCharacterP2) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
        Player2Character = CharacterDB.GetCharacter(SelectedCharacterP2);
    }

    public void InstantiatePlayer1() {
        Vector3 Player1Spawn = new Vector3(-5.93f, -2.41f, 0);

        Player1Instance = Instantiate(Player1Character.CharacterPrefab, Player1Spawn, Quaternion.identity);
        Player1Animator = Player1Instance.GetComponent<Animator>();
    }

    public void InstantiatePlayer2() {
        Vector3 Player2Spawn = new Vector3(5.93f, -2.41f, 0);

        Player2Instance = Instantiate(Player2Character.CharacterPrefab, Player2Spawn, Quaternion.Euler(0, 180, 0));
        Player2Animator = Player2Instance.GetComponent<Animator>();
    }
}
