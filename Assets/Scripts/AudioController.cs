using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
    [SerializeField] private Slider SliderSong;
    [SerializeField] private Slider SliderSoundEffects;
    private AudioSource BackgroundSong;
    private AudioSource SoundEffects;

    void Start() {
        BackgroundSong = FindObjectOfType<AudioSource>();
        SoundEffects = FindObjectsOfType<AudioSource>()[1];

        SliderSong.value = BackgroundSong.volume;
        SliderSoundEffects.value = SoundEffects.volume;
    }

    public void SongController(float value) {
        BackgroundSong.volume = value;
        SliderSong.value = value;
    }

    public void SoundEffectsController(float value) {
        SoundEffects.volume = value;
        SliderSoundEffects.value = value;
    }
}
