using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour {

    [SerializeField] private AudioSource CombatMusic;
    [SerializeField] private AudioSource YouDiedMusic;
    [SerializeField] private AudioSource HitSoundEffect;
    [SerializeField] private AudioSource AttackSoundEffect;
    [SerializeField] private AudioSource ClickSoundEffect;
    [SerializeField] private AudioSource SelectSoundEffect;

    void Start() {
        CombatMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        YouDiedMusic.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        HitSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        AttackSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        ClickSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        SelectSoundEffect.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
    }

    public void PlayCombatMusic() {
        YouDiedMusic.Stop();
        CombatMusic.Play();
    }

    public void PauseMusic(bool IsPaused) {
        if (IsPaused) {
            CombatMusic.Pause();
        }

        else {
            CombatMusic.UnPause();
        }
    }

    public void PlayYouDiedMusic() {
        YouDiedMusic.Play();
        CombatMusic.Stop();
    }

    public void PlayHitSoundEffect(string Player) {
        if (Player == "Player1") {
            AttackSoundEffect.Play();
        }

        if (Player == "Player2") {
            HitSoundEffect.Play();
        }
    }
}
