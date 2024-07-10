using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class KeySequenceController : MonoBehaviour {
    [SerializeField] private KeySequenceGenerator SequenceGenerator;
    [SerializeField] private GameManager Manager;
    public List<KeyCode> Player1Sequence = new List<KeyCode>();
    public KeyCode[] CurrentSequence;
    public KeyCode[] KeyCodesP1 = new KeyCode[4];
    public KeyCode[] KeyCodesP2 = new KeyCode[4];
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
        Manager.UpdateSequence(CurrentSequence);
    }

    void Update() {
        if (Input.anyKeyDown) {
            foreach (KeyCode key in KeyCodesP1) {
                if (Input.GetKeyDown(key)) {
                    Player1Sequence.Add(key);
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
                Debug.Log("Sequência correta!");
                Manager.Player1Attack();
            }
            
            if (SequenceMatch == 0) {
                Debug.Log("Sequência incorreta!");
                Manager.Player2Attack();

                Player1Sequence.Clear();
                CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
                Manager.UpdateSequence(CurrentSequence);
            }
        }

        if (Player1Sequence.Count == CurrentSequence.Length) {
            Player1Sequence.Clear();
            CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
            Manager.UpdateSequence(CurrentSequence);
        }
    }

    public void LoadKeyCodes() {
        for (int i = 0; i < KeyCodesP1.Length; i++) {
            KeyCodesP1[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP1_" + i, (int)KeyCode.None);
        }

        for (int i = 0; i < KeyCodesP2.Length; i++) {
            KeyCodesP2[i] = (KeyCode)PlayerPrefs.GetInt("KeyCodeP2_" + i, (int)KeyCode.None);
        }
    }

    private void LoadCharacter() {
        SelectedCharacterP1 = PlayerPrefs.GetInt("SelectedCharacterP1");
    }

    private void UpdateCharacter(int SelectedCharacterP1) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
    }
}
