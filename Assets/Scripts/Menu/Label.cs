using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label : MonoBehaviour {
    [SerializeField] public string OnSelectionLabel;
    [SerializeField] public string OnClickLabel;

    public string GetOnSelectionLabel() {
        return OnSelectionLabel;
    }

    public string GetOnClickLabel() {
        return OnClickLabel;
    }
}
