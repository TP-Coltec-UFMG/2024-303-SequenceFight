using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class KeySequenceGenerator : MonoBehaviour {
    public KeyCode[] GenerateSequence(KeyCode[] AvailableKeys) {
        KeyCode[] Sequence = new KeyCode[5];
        
        for (int i = 0; i < Sequence.Length; i++) {
            int RandomIndex = Random.Range(0, AvailableKeys.Length);
            Sequence[i] = AvailableKeys[RandomIndex];
        }

        return Sequence;
    }
}
