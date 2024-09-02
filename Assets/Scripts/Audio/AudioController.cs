using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioController : MonoBehaviour {
    [SerializeField] private Slider SliderBackgroundMusic;
    [SerializeField] private Slider SliderSoundEffects;
    [SerializeField] private AudioSource BackgroundMusic;
    [SerializeField] private AudioSource ClickSoundEffect;
    [SerializeField] private AudioSource SelectSoundEffect;
    [SerializeField] private AudioSource HitSoundEffect;
    [SerializeField] private AudioSource AttackSoundEffect;

    private bool SoundEffectsFeedback = false;

    void Start() {
        if (!PlayerPrefs.HasKey("BackgroundMusicVolume")) {
            PlayerPrefs.SetFloat("BackgroundMusicVolume", 0f);
        }

        if (!PlayerPrefs.HasKey("SoundEffectsVolume")) {
            PlayerPrefs.SetFloat("SoundEffectsVolume", 0f);
        }

        LoadAudioSettings();

        SoundEffectsFeedback = true;
    }

    public void BackgroundMusicController(float Value) {
        BackgroundMusic.volume = Value;
        SliderBackgroundMusic.value = Value;

        SaveAudioSettings();
    }

    public void SoundEffectsController(float Value) {
        ClickSoundEffect.volume = Value;
        SelectSoundEffect.volume = Value;
        HitSoundEffect.volume = Value;
        AttackSoundEffect.volume = Value;

        SliderSoundEffects.value = Value;

        if (SoundEffectsFeedback) {
            HitSoundEffect.Play();
        }

        SaveAudioSettings();
    }

    public void SaveAudioSettings() {
        PlayerPrefs.SetFloat("BackgroundMusicVolume", BackgroundMusic.volume);
        PlayerPrefs.SetFloat("SoundEffectsVolume", ClickSoundEffect.volume);
    }

    public void LoadAudioSettings() {
        BackgroundMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");

        ClickSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");;
        SelectSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");;
        HitSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");;
        AttackSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");;
        
        SliderBackgroundMusic.value = BackgroundMusic.volume;
        SliderSoundEffects.value = ClickSoundEffect.volume; 
    }
}