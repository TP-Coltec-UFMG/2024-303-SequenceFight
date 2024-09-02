using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class KeySequenceControllerBlind : MonoBehaviour {
    [SerializeField] private KeySequenceGenerator SequenceGenerator;
    [SerializeField] private GameManagerBlind Manager;
    public List<KeyCode> Player1Sequence = new List<KeyCode>();
    public KeyCode[] CurrentSequence;
    public KeyCode[] KeyCodesP1 = new KeyCode[4];
    private int SequenceMatch;
    public int SelectedCharacterP1;
    public CharacterDatabase CharacterDB;
    public Character Player1Character;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);
        CurrentSequence = new KeyCode[Player1Character.SequenceLength];

        LoadKeyCodes();
        CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);

        string Str = "";

        foreach (KeyCode Key in CurrentSequence) {
            Str += Key.ToString() + " ";
        }

        Manager.CurrentSequence = Str;
    }

    void Update() {
        if (Input.anyKeyDown) {
            foreach (KeyCode Key in KeyCodesP1) {
                if (Input.GetKeyDown(Key)) {
                    Player1Sequence.Add(Key);
                    CheckSequence();
                    break;
                }
            }
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
                }
            }

            if (SequenceMatch == Player1Character.SequenceLength + 1) {
                Manager.Player1Attack();
            }
            
            if (SequenceMatch == 0) {
                Manager.Player2Attack();

                if (Manager.IsPlayerAlive()) {
                    Player1Sequence.Clear();

                    CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);

                    string Str = "";

                    foreach (KeyCode Key in CurrentSequence) {
                        Str += Key.ToString() + " ";
                    }

                    Manager.CurrentSequence = Str;

                    Manager.Speak(Str.Replace(" ", PlayerPrefs.GetString("Rate")));
                }

                if (!Manager.IsPlayerAlive()) {
                    Player1Sequence.Clear();
                }
            }
        }

        if (Player1Sequence.Count == CurrentSequence.Length) {
            Player1Sequence.Clear();

            CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);

            string Str = "";

            foreach (KeyCode Key in CurrentSequence) {
                Str += Key.ToString() + " ";
            }

            Manager.CurrentSequence = Str;

            Manager.Speak(Str.Replace(" ", PlayerPrefs.GetString("Rate")));
        }
    }

    public void LoadKeyCodes() {
        for (int i = 0; i < KeyCodesP1.Length; i++) {
            KeyCodesP1[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP1_" + i, (int)KeyCode.None);
        }
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
    }
}
