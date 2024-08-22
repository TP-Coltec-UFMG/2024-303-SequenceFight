using UnityEngine;
using System.Collections.Generic;

public class KeySequenceControllerInfinite : MonoBehaviour {
    [SerializeField] private KeySequenceGenerator SequenceGenerator;
    [SerializeField] private GameManagerInfinite Manager;
    public List<KeyCode> Player1Sequence = new List<KeyCode>();
    public KeyCode[] CurrentSequence;
    public KeyCode[] KeyCodesP1 = new KeyCode[4];
    private int SequenceMatch;
    public int SelectedCharacterP1;
    public CharacterDatabase CharacterDB;
    public Character Player1Character;
    private float TimeLeft;
    private float[] TimeLimits = new float[] { 15f, 14f, 13f, 12f, 11f, 10f, 9f, 8f, 7f, 6f, 5f };

    void Start() {
        LoadCharacter();
        UpdateCharacter(SelectedCharacterP1);
        CurrentSequence = new KeyCode[Player1Character.SequenceLength];

        LoadKeyCodes();

        CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
        Manager.UpdateSequence(CurrentSequence);

        ResetTimeLimit();
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

        TimeLeft -= Time.deltaTime;
        Manager.UIManager.UpdateTimer(TimeLeft);

        if (TimeLeft <= 0) {
            Manager.Player2Attack();

            ResetTimeLimit();

            Player1Sequence.Clear();

            CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1, Player1Character.SequenceLength);
            Manager.UpdateSequence(CurrentSequence);
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

                ResetTimeLimit();
            }
            
            if (SequenceMatch == 0) {
                Manager.Player2Attack();
                ResetTimeLimit();

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

    void ResetTimeLimit() {
        int index = Mathf.Min(Manager.EnemyCount, TimeLimits.Length - 1);
        TimeLeft = TimeLimits[index];
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
