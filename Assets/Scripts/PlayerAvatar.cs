using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerAvatar : MonoBehaviour {
    private string P1;
    private string P2;

    void Start() {
        
    }

    public void SaveAvatarToJson(string filePath) {
        AvatarData Data = new AvatarData();
        Data.AvatarP1 = P1;
        Data.AvatarP2 = P2;

        string json = JsonUtility.ToJson(Data);

        File.WriteAllText(filePath, json);
    }

    public void LoadAvatarFromJson(string filePath) {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);

            AvatarData Data = JsonUtility.FromJson<AvatarData>(json);

            P1 = Data.AvatarP1;
            P2 = Data.AvatarP2;

        }
        
        else {
            Debug.LogError("O arquivo JSON não existe: " + filePath);
        }
    }
}
