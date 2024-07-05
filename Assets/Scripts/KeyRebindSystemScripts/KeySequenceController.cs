using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class KeySequenceController : MonoBehaviour {
    [SerializeField] private KeySequenceGenerator SequenceGenerator;
    [SerializeField] private GameManager Manager;
    public List<KeyCode> PlayerSequence = new List<KeyCode>();
    public KeyCode[] CurrentSequence = new KeyCode[4];
    public KeyCode[] KeyCodesP1 = new KeyCode[4];
    public KeyCode[] KeyCodesP2 = new KeyCode[4];
    private int SequenceMatch;

    void Start() {
        LoadKeyCodes();
        CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1);
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

            if (SequenceMatch == 6) {
                Debug.Log("Sequência correta!");
                Manager.PlayerAttack();
            }
            
            if (SequenceMatch == 0) {
                Debug.Log("Sequência incorreta!");
                Manager.EnemyAttack();

                PlayerSequence.Clear();
                CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1);
                Manager.UpdateSequence(CurrentSequence);
            }
        }

        if (PlayerSequence.Count == CurrentSequence.Length) {
            PlayerSequence.Clear();
            CurrentSequence = SequenceGenerator.GenerateSequence(KeyCodesP1);
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
}
