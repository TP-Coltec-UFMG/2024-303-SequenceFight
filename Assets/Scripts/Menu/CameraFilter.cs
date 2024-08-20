using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFilter : MonoBehaviour {
    public ColorBlindFilter Filter;

    void Start() {
        Filter = GetComponent<ColorBlindFilter>();
    }
}