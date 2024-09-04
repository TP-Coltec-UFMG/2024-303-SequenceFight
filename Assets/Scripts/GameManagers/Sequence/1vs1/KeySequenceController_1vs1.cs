using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class KeySequenceController1vs1 : MonoBehaviour {
    [SerializeField] private KeySequenceGenerator SequenceGenerator;
    [SerializeField] public GameManager1vs1 Manager;

    public List<KeyCode> Player1Sequence = new List<KeyCode>();
    public List<KeyCode> Player2Sequence = new List<KeyCode>();

    public KeyCode[] CurrentSequenceP1;
    public KeyCode[] CurrentSequenceP2;

    public KeyCode[] KeyCodesP1 = new KeyCode[4];
    public KeyCode[] KeyCodesP2 = new KeyCode[4];

    private int SequenceMatchP1;
    private int SequenceMatchP2;

    public int SelectedCharacterP1;
    public int SelectedCharacterP2;

    public CharacterDatabase CharacterDB;
    public Character Player1Character;
    public Character Player2Character;

    private bool IsPaused;

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1, SelectedCharacterP2);

        CurrentSequenceP1 = new KeyCode[Player1Character.SequenceLength];
        CurrentSequenceP2 = new KeyCode[Player2Character.SequenceLength];

        LoadKeyCodes();

        CurrentSequenceP1 = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
        CurrentSequenceP2 = SequenceGenerator.GenerateSequence(KeyCodesP2, Player2Character.SequenceLength);

        Manager.UpdateSequenceP1(CurrentSequenceP1, -1, -1);
        Manager.UpdateSequenceP2(CurrentSequenceP2, -1, -1);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        if (IsPaused) {
            return;
        }

        if (Input.anyKeyDown) {
            foreach (KeyCode key in KeyCodesP1) {
                if (Input.GetKeyDown(key)) {
                    Player1Sequence.Add(key);
                    CheckSequenceP1();
                    break;
                }
            }

            foreach (KeyCode key in KeyCodesP2) {
                if (Input.GetKeyDown(key)) {
                    Player2Sequence.Add(key);
                    CheckSequenceP2();
                    break;
                }
            }
        }
    }

    void CheckSequenceP1() {
        SequenceMatchP1 = 1; 

        if (Player1Sequence.Count <= CurrentSequenceP1.Length) {
            for (int i = 0; i < Player1Sequence.Count; i++) {
                if (Player1Sequence[i] != CurrentSequenceP1[i]) {
                    SequenceMatchP1 = 0;
                    break;
                }

                else {
                    SequenceMatchP1++;

                    Manager.UpdateSequenceP1(CurrentSequenceP1, i, CurrentSequenceP1.Length);
                }
            }

            if (SequenceMatchP1 == Player1Character.SequenceLength + 1) {
                Manager.Player1Attack();
            }
            
            if (SequenceMatchP1 == 0) {
                Player1Sequence.Clear();
                CurrentSequenceP1 = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
                Manager.UpdateSequenceP1(CurrentSequenceP1, -1, -1);
            }
        }

        if (Player1Sequence.Count == CurrentSequenceP1.Length) {
            Player1Sequence.Clear();
            CurrentSequenceP1 = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
            Manager.UpdateSequenceP1(CurrentSequenceP1, -1, -1);
        }
    }

    void CheckSequenceP2() {
        SequenceMatchP2 = 1; 

        if (Player2Sequence.Count <= CurrentSequenceP2.Length) {
            for (int i = 0; i < Player2Sequence.Count; i++) {
                if (Player2Sequence[i] != CurrentSequenceP2[i]) {
                    SequenceMatchP2 = 0;
                    break;
                }

                else {
                    SequenceMatchP2++;

                    Manager.UpdateSequenceP2(CurrentSequenceP2, i, CurrentSequenceP2.Length);
                }
            }

            if (SequenceMatchP2 == Player2Character.SequenceLength + 1) {
                Manager.Player2Attack();
            }
            
            if (SequenceMatchP2 == 0) {
                Player2Sequence.Clear();
                CurrentSequenceP2 = SequenceGenerator.GenerateSequence(KeyCodesP2, Player2Character.SequenceLength);
                Manager.UpdateSequenceP2(CurrentSequenceP2, -1, -1);
            }
        }

        if (Player2Sequence.Count == CurrentSequenceP2.Length) {
            Player2Sequence.Clear();
            CurrentSequenceP2 = SequenceGenerator.GenerateSequence(KeyCodesP2, Player2Character.SequenceLength);
            Manager.UpdateSequenceP2(CurrentSequenceP2, -1, -1);
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
        SelectedCharacterP2 = PlayerPrefs.GetInt("SelectedCharacterP2");
    }

    private void UpdateCharacter(int SelectedCharacterP1, int SelectedCharacterP2) {
        Player1Character = CharacterDB.GetCharacter(SelectedCharacterP1);
        Player2Character = CharacterDB.GetCharacter(SelectedCharacterP2);
    }

    public void TogglePause() {
        if (!Manager.RestartGameBool) {
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0f : 1f;
            
            Manager.IsPaused = IsPaused;
            Manager.UIManager.TogglePauseMenu(IsPaused);
            Manager.AudioController.PauseMusic(IsPaused);
        }
    }
}
