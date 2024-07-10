using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI PlayerHealthUI;
    [SerializeField] private TextMeshProUGUI EnemyHealthUI;
    [SerializeField] private TextMeshProUGUI RecordUI;
    [SerializeField] private TextMeshProUGUI StreakUI;
    [SerializeField] private TextMeshProUGUI CurrentSequence;
    [SerializeField] private TextMeshProUGUI NewRecordText;

    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private GameObject EnemyHit;
    [SerializeField] private float HitDuration;

    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject NewRecordUI;
    [SerializeField] private GameObject RestartGameUI;

    public void UpdateHealth(float PlayerHealth, float EnemyHealth) {
        PlayerHealthUI.text = "" + PlayerHealth + "";
        EnemyHealthUI.text = "" + EnemyHealth + "";
    }

    public void UpdateRecord(int StreakInt, int RecordInt) {
        if (StreakInt >= RecordInt) {
            RecordInt = StreakInt;
            PlayerPrefs.SetInt("Record", StreakInt);
        }

        StreakUI.text = "" + StreakInt + "";
        RecordUI.text = "" + RecordInt + "";
    }

    public void UpdateSequence(string SequenceString) {
        CurrentSequence.text = SequenceString;
    }

    public void RestartGame() {
        GameUI.SetActive(true);
        RestartGameUI.SetActive(false);
        NewRecordUI.SetActive(false);
    }

    public void ActivateRestartGameUI(int StreakInt, int RecordInt) {
        GameUI.SetActive(false);
        RestartGameUI.SetActive(true);

        if (StreakInt == RecordInt) {
            NewRecordText.text = RecordUI.text;
            NewRecordUI.SetActive(true);
        }
    }

    public void ShowPlayerHit() {
        StartCoroutine(HitIndicator(PlayerHit));
    }

    public void ShowEnemyHit() {
        StartCoroutine(HitIndicator(EnemyHit));
    }

    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
    }
}
