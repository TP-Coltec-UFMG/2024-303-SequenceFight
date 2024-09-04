using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Diagnostics.Tracing;
using UnityEngine.UIElements;

public class KeySequenceControllerWord : MonoBehaviour {
    [SerializeField] private WordGenerator WordGenerator;
    [SerializeField] private GameManagerWord Manager;
    public List<char> Player1Sequence = new List<char>();
    public string CurrentSequence;
    private int SequenceMatch;
    public int SelectedCharacterP1;
    public CharacterDatabase CharacterDB;
    public Character Player1Character;
    private float TimeLeft;
    private float[] TimeLimits = new float[] { 10f, 9.5f, 9f, 8.5f, 8f, 7.5f, 7f, 6.5f, 6f, 5.5f, 5f, 4.8f, 4.6f, 4.4f, 4.2f, 4f, 3.8f, 3.6f, 3.4f, 3.2f, 3f, 2.9f, 2.8f, 2.7f, 2.6f, 2.5f };
    public Event E;
    private bool IsPaused;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);

        if (Player1Character.SequenceLength <= 5) {
            WordGenerator.Initiate(5);
        }

        else if (Player1Character.SequenceLength <= 11) {
            WordGenerator.Initiate(7);
        }

        else if (Player1Character.SequenceLength <= 15) {
            WordGenerator.Initiate(9);
        }

        else {
            WordGenerator.Initiate(20);
        }

        CurrentSequence = WordGenerator.GenerateWord();

        Manager.UpdateWord(CurrentSequence, -1, -1);

        ResetTimeLimit();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        if (IsPaused) {
            return;
        }

        TimeLeft -= Time.deltaTime;
        Manager.UIManager.UpdateTimer(TimeLeft);

        if (TimeLeft <= 0) {
            Manager.Player2Attack();

            ResetTimeLimit();

            Player1Sequence.Clear();

            CurrentSequence = WordGenerator.GenerateWord();
            Manager.UpdateWord(CurrentSequence, -1, -1);
        }
    }

    void CheckSequence() {
        SequenceMatch = 1;

        if (Player1Sequence.Count <= CurrentSequence.Length) {
            for (int i = 0; i < Player1Sequence.Count; i++) {
                if (Player1Sequence[i] != CurrentSequence[i]) {
                    SequenceMatch = 0;
                    break;
                }

                else {
                    SequenceMatch++;

                    Manager.UpdateWord(CurrentSequence, i, CurrentSequence.Length);
                }
            }

            if (SequenceMatch == CurrentSequence.Length + 1) {
                Manager.Player1Attack();
                ResetTimeLimit();
            }
            
            if (SequenceMatch == 0) {
                Manager.Player2Attack();
                ResetTimeLimit();

                Player1Sequence.Clear();

                CurrentSequence = WordGenerator.GenerateWord();
                Manager.UpdateWord(CurrentSequence, -1, -1);
            }
        }

        if (Player1Sequence.Count == CurrentSequence.Length) {
            Player1Sequence.Clear();

            CurrentSequence = WordGenerator.GenerateWord();
            Manager.UpdateWord(CurrentSequence, -1, -1);
        }
    }

    void OnGUI() {
        if (!IsPaused && !Manager.RestartGameBool) {
            E = Event.current;

            if (E != null && E.isKey) {
                KeyCode Key = E.keyCode;

                if ((Key >= KeyCode.A) && (Key <= KeyCode.Z) && Input.GetKeyDown(Key)) {
                    Player1Sequence.Add((char)Key);
                    CheckSequence();
                }
            }
        }
    }

    void ResetTimeLimit() {
        int Index = Mathf.Min(Manager.EnemyCount, TimeLimits.Length - 1);
        TimeLeft = TimeLimits[Index];
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
    }

    public void TogglePause() {
        if (!Manager.RestartGameBool) {
            IsPaused = !IsPaused;
            Manager.VerifyRecord();
            Time.timeScale = IsPaused ? 0f : 1f;
            Manager.UIManager.TogglePauseMenu(IsPaused);
            Manager.AudioController.PauseMusic(IsPaused);
        }
    }
}
