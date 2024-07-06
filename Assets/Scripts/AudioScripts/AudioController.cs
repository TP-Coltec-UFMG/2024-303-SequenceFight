using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioController : MonoBehaviour {
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderSoundEffects;
    [SerializeField] private AudioSource BackgroundMusic;
    private float SoundEffectsVolume;

    void Start() {
        BackgroundMusic = FindObjectOfType<AudioSource>();

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
        SoundEffectsVolume = value;
        SliderSoundEffects.value = value;

        SaveAudioSettings();
    }

    public void SaveAudioSettings() {
        PlayerPrefs.SetFloat("BackgroundMusicVolume", BackgroundMusic.volume);
        PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolume);
    }

    public void LoadAudioSettings() {
        BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        SoundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume");

        SliderMusic.value = BackgroundMusic.volume;
        SliderSoundEffects.value = SoundEffectsVolume;
    }
}