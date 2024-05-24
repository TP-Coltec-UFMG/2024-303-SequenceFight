using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    private float PlayerLife = 100f;
    private float EnemyLife = 100f;
    [SerializeField] private TextMeshProUGUI PlayerLifeUI;
    [SerializeField] private TextMeshProUGUI EnemyLifeUI;
    [SerializeField] private TextMeshProUGUI CurrentSequence;
    private AudioSource HitSoundEffect;
    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private GameObject EnemyHit;
    [SerializeField] private float HitDuration;

    void Start() {
        UpdateLifes();
        HitSoundEffect = FindSoundEffect();
    }

    private AudioSource FindSoundEffect() {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSources) {
            if (source.gameObject.name == "HitSoundEffect") {
                return source;
            }
        }

        Debug.LogWarning("HitSoundEffect AudioSource not found.");
        return null;
    }

    public void PlayerAttack() {
        EnemyLife -= 10f;
        HitSoundEffect.Play();
        StartCoroutine(HitIndicator(PlayerHit));

        if (EnemyLife == 0) {
            Debug.Log("Player win");
            RestartGame();
        }

        else {
            UpdateLifes();
        }
    }

    public void EnemyAttack() {
        PlayerLife -= 10f;
        HitSoundEffect.Play();
        StartCoroutine(HitIndicator(EnemyHit));

        if (PlayerLife == 0) {
            Debug.Log("Enemy win");
            RestartGame();
        }

        else {
            UpdateLifes();
        }
    }

    void UpdateLifes() {
        PlayerLifeUI.text = " " + PlayerLife + " ";
        EnemyLifeUI.text = " " + EnemyLife + " ";
    }

    public void UpdateSequence(KeyCode[] sequence) {
        string sequenceString = " ";

        foreach (KeyCode key in sequence) {
            sequenceString += key.ToString() + " ";
        }

        CurrentSequence.text = sequenceString;
    }

    void RestartGame() {
        PlayerLife = 100f;
        EnemyLife = 100f;

        UpdateLifes();
    }

    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
    }
}
