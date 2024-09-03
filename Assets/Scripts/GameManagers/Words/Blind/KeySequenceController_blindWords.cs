using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Diagnostics.Tracing;
using UnityEngine.UIElements;

public class KeySequenceControllerBlindWord : MonoBehaviour {
    [SerializeField] private WordGenerator WordGenerator;
    [SerializeField] private GameManagerBlindWord Manager;
    public List<char> Player1Sequence = new List<char>();
    public string CurrentWord;
    private int SequenceMatch;
    public int SelectedCharacterP1;
    public CharacterDatabase CharacterDB;
    public Character Player1Character;
    public Event E;

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

        CurrentWord = WordGenerator.GenerateWord();

        Manager.UpdateWord(CurrentWord);
    }

    void CheckSequence() {
        SequenceMatch = 1;

        if (Player1Sequence.Count <= CurrentWord.Length) {
            for (int i = 0; i < Player1Sequence.Count; i++) {
                if (Player1Sequence[i] != CurrentWord[i]) {
                    SequenceMatch = 0;
                    break;
                }

                else {
                    SequenceMatch++;
                }
            }
            
            if (SequenceMatch == CurrentWord.Length + 1) {
                Manager.Player1Attack();
            }

            if (SequenceMatch == 0) {
                Manager.Player2Attack();

                if (Manager.IsPlayerAlive()) {
                    Player1Sequence.Clear();

                    CurrentWord = WordGenerator.GenerateWord();
                    Manager.UpdateWord(CurrentWord);

                    Manager.Speak(CurrentWord);
                }

                if (!Manager.IsPlayerAlive()) {
                    Player1Sequence.Clear();
                }
            }
        }

        if (Player1Sequence.Count == CurrentWord.Length) {
            Player1Sequence.Clear();

            CurrentWord = WordGenerator.GenerateWord();
            Manager.UpdateWord(CurrentWord);

            Manager.Speak(CurrentWord);
        }
    }

    void OnGUI() {
        if (!Manager.RestartGameBool) {
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

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
    }
}
