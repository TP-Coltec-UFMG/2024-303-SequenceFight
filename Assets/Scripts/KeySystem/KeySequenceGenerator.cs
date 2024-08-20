using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class KeySequenceGenerator : MonoBehaviour {
    public KeyCode[] GenerateSequence(KeyCode[] AvailableKeys, int SequenceLength) {
        KeyCode[] Sequence = new KeyCode[SequenceLength];
        
        for (int i = 0; i < Sequence.Length; i++) {
            int RandomIndex = Random.Range(0, AvailableKeys.Length);
            Sequence[i] = AvailableKeys[RandomIndex];
        }

        return Sequence;
    }
}
