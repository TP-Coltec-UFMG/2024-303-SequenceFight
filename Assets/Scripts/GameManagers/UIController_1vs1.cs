using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController1vs1 : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI Player1HealthUI;
    [SerializeField] private TextMeshProUGUI Player2HealthUI;

    [SerializeField] private TextMeshProUGUI CurrentSequenceP1;
    [SerializeField] private TextMeshProUGUI CurrentSequenceP2;

    [SerializeField] private GameObject PlayerHit;
    [SerializeField] private float HitDuration;

    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject RestartGameUI;
    [SerializeField] private GameObject P1win;
    [SerializeField] private GameObject P2win;

    public void UpdateHealth(float Player1Health, float Player2Health) {
        Player1HealthUI.text = "" + Player1Health + "";
        Player2HealthUI.text = "" + Player2Health + "";
    }

    public void UpdateSequenceP1(string SequenceStringP1) {
        CurrentSequenceP1.text = SequenceStringP1;
    }

    public void UpdateSequenceP2(string SequenceStringP2) {
        CurrentSequenceP2.text = SequenceStringP2;
    }

    public void RestartGame() {
        GameUI.SetActive(true);
        RestartGameUI.SetActive(false);
    }

    public void ActivateRestartGameUI(bool P1winBool, bool P2winBool) {
        GameUI.SetActive(false);
        RestartGameUI.SetActive(true);

        P1win.SetActive(P1winBool);
        P2win.SetActive(P2winBool);
    }

    public void ShowPlayerHit() {
        StartCoroutine(HitIndicator(PlayerHit));
    }

    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
    }
}
