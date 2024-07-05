using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioController : MonoBehaviour {
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderSoundEffects;
    private AudioSource BackgroundMusic;
    private AudioSource SoundEffects;

    void Start() {
        BackgroundMusic = FindObjectOfType<AudioSource>();
        SoundEffects = FindObjectsOfType<AudioSource>()[1];

        if (!PlayerPrefs.HasKey("BackgroundMusicVolume")) {
            PlayerPrefs.SetFloat("BackgroundMusicVolume", (float)0.5);
        }

        if (!PlayerPrefs.HasKey("SoundEffectsVolume")) {
            PlayerPrefs.SetFloat("SoundEffectsVolume", (float)0.5);
        }

        LoadAudioSettings();
    }

    public void MusicController(float value) {
        BackgroundMusic.volume = value;
        SliderMusic.value = value;

        SaveAudioSettings();
    }

    public void SoundEffectsController(float value) {
        SoundEffects.volume = value;
        SliderSoundEffects.value = value;

        SaveAudioSettings();
    }

    public void SaveAudioSettings() {
        PlayerPrefs.SetFloat("BackgroundMusicVolume", BackgroundMusic.volume);
        PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffects.volume);
    }

    public void LoadAudioSettings() {
        BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        SoundEffects.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");

        SliderMusic.value = BackgroundMusic.volume;
        SliderSoundEffects.value = SoundEffects.volume;
    }
}