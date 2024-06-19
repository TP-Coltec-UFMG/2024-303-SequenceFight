using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioController : MonoBehaviour {
    [SerializeField] private Slider SliderSong;
    [SerializeField] private Slider SliderSoundEffects;
    private AudioSource BackgroundSong;
    private AudioSource SoundEffects;

    void Start() {
        BackgroundSong = FindObjectOfType<AudioSource>();
        SoundEffects = FindObjectsOfType<AudioSource>()[1];

        LoadAudioSettingsFromJson("Assets/Scripts/jsons/AudioVolume.json");
    }

    public void SongController(float value) {
        BackgroundSong.volume = value;
        SliderSong.value = value;

        SaveAudioSettingsToJson("Assets/Scripts/jsons/AudioVolume.json");
    }

    public void SoundEffectsController(float value) {
        SoundEffects.volume = value;
        SliderSoundEffects.value = value;

        SaveAudioSettingsToJson("Assets/Scripts/jsons/AudioVolume.json");
    }

    public void SaveAudioSettingsToJson(string filePath) {
        AudioVolume Data = new AudioVolume();
        Data.Song = BackgroundSong.volume;
        Data.SoundEffects = SoundEffects.volume;

        string json = JsonUtility.ToJson(Data);

        File.WriteAllText(filePath, json);
    }

    public void LoadAudioSettingsFromJson(string filePath) {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);

            AudioVolume Data = JsonUtility.FromJson<AudioVolume>(json);

            BackgroundSong.volume = Data.Song;
            SoundEffects.volume = Data.SoundEffects;
            SliderSong.value = Data.Song;
            SliderSoundEffects.value = Data.SoundEffects;
        }
        else {
            Debug.LogError("O arquivo JSON não existe: " + filePath);
        }
    }
}