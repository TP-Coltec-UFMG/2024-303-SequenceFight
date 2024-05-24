using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
    public static DontDestroy Instance;
    void Awake() {
        if (Instance == null) {
            Instance = this;
        }

        else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
