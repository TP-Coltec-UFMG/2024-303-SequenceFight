using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour {

    [SerializeField] private AudioSource CombatMusic;
    [SerializeField] private AudioSource YouDiedMusic;
    [SerializeField] private AudioSource HitSoundEffect;

    void Start() {
        CombatMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        YouDiedMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        HitSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
    }

    public void PlayCombatMusic() {
        YouDiedMusic.Stop();
        CombatMusic.Play();
    }

    public void PlayYouDiedMusic() {
        YouDiedMusic.Play();
        CombatMusic.Stop();
    }

    public void PlayHitSoundEffect() {
        HitSoundEffect.Play();
    }
}
