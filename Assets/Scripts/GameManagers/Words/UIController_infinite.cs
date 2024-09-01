using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIControllerWord : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI PlayerHealthUI;
    [SerializeField] private TextMeshProUGUI EnemyHealthUI;
    [SerializeField] private TextMeshProUGUI RecordUI;
    [SerializeField] private TextMeshProUGUI StreakUI;
    [SerializeField] private TextMeshProUGUI CurrentSequence;
    [SerializeField] private TextMeshProUGUI NewRecordText;
    [SerializeField] private TextMeshProUGUI TimerUI; 
    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private GameObject EnemyHit;
    [SerializeField] private float HitDuration;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject NewRecordUI;
    [SerializeField] private GameObject RestartGameUI;
    [SerializeField] private GameObject RestartGameButton;
    [SerializeField] private GameObject NonLabelObject;

    public void UpdateHealth(float PlayerHealth, float EnemyHealth) {
        PlayerHealthUI.text = "" + PlayerHealth + "";
        EnemyHealthUI.text = "" + EnemyHealth + "";
    }

    public int UpdateRecord(int StreakInt, int RecordInt) {
        if (StreakInt >= RecordInt) {
            RecordInt = StreakInt;
            PlayerPrefs.SetInt("RecordInfinite", StreakInt);
        }

        StreakUI.text = "" + StreakInt + "";
        RecordUI.text = "" + RecordInt + "";

        return RecordInt;
    }

    public void UpdateSequence(string SequenceString) {
        CurrentSequence.text = SequenceString;
    }

    public void UpdateTimer(float TimeLeft) {
        TimerUI.text = TimeLeft.ToString("F1") + "s";
    }

    public void RestartGame() {
        GameUI.SetActive(true);
        RestartGameUI.SetActive(false);
        NewRecordUI.SetActive(false);

        EventSystem.current.SetSelectedGameObject(NonLabelObject);
    }

    public void ActivateRestartGameUI(int StreakInt, int RecordInt) {
        GameUI.SetActive(false);
        RestartGameUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(RestartGameButton);

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