using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class KeySequenceController : MonoBehaviour {
    [SerializeField] private KeySequenceGenerator SequenceGenerator;
    [SerializeField] private GameManager Manager;
    public List<KeyCode> PlayerSequence = new List<KeyCode>();
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
                    PlayerSequence.Add(key);
                    CheckSequence();
                    break;
                }
            }
        }
    }

    void CheckSequence() {
        SequenceMatch = 1; 

        if (PlayerSequence.Count <= CurrentSequence.Length) {
            for (int i = 0; i < PlayerSequence.Count; i++) {
                if (PlayerSequence[i] != CurrentSequence[i]) {
                    SequenceMatch = 0;
                    break;
                }

                else {
                    SequenceMatch++;
                }
            }

            if (SequenceMatch == Player1Character.SequenceLength + 1) {
                Debug.Log("Sequência correta!");
                Manager.PlayerAttack();
            }
            
            if (SequenceMatch == 0) {
                Debug.Log("Sequência incorreta!");
                Manager.EnemyAttack();

                PlayerSequence.Clear();
                CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
                Manager.UpdateSequence(CurrentSequence);
            }
        }

        if (PlayerSequence.Count == CurrentSequence.Length) {
            PlayerSequence.Clear();
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
