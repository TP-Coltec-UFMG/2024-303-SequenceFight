using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField] private GameObject ActiveAbility;
    [SerializeField] private GameObject Barrier;
    [SerializeField] private GameObject SwapHealth;
    [SerializeField] private GameObject DoubleDamage;
    [SerializeField] private GameObject Vampirism;

    [SerializeField] private GameObject ActiveAbilityP1;
    [SerializeField] private GameObject BarrierP1;
    [SerializeField] private GameObject SwapHealthP1;
    [SerializeField] private GameObject DoubleDamageP1;
    [SerializeField] private GameObject VampirismP1;

    [SerializeField] private GameObject ActiveAbilityP2;
    [SerializeField] private GameObject BarrierP2;
    [SerializeField] private GameObject SwapHealthP2;
    [SerializeField] private GameObject DoubleDamageP2;
    [SerializeField] private GameObject VampirismP2;

    [SerializeField] private GameObject RestartGameButton;
    [SerializeField] private GameObject NonLabelObject;

    [SerializeField] private GameObject PauseMenuUI; 
    [SerializeField] private GameObject PauseMenuFirstButton; 
    
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

        EventSystem.current.SetSelectedGameObject(NonLabelObject);
    }

    public void ActivateRestartGameUI(bool P1winBool, bool P2winBool) {
        GameUI.SetActive(false);
        RestartGameUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(RestartGameButton);

        P1win.SetActive(P1winBool);
        P2win.SetActive(P2winBool);
    }

    public void ShowPlayerHit() {
        StartCoroutine(HitIndicator(PlayerHit));
    }

    public void ActivateAbilityUI(string Ability) {
        DeactivateAbilityUI();

        if (Ability == "Barrier") {
            Barrier.SetActive(true);
        }

        if (Ability == "SwapHealth") {
            SwapHealth.SetActive(true);
        }

        if (Ability == "DoubleDamage") {
            DoubleDamage.SetActive(true);
        }

        if (Ability == "Vampirism") {
            Vampirism.SetActive(true);
        }
    }

    public void DeactivateAbilityUI() {
        Barrier.SetActive(false);

        SwapHealth.SetActive(false);   

        DoubleDamage.SetActive(false); 

        Vampirism.SetActive(false);
    }

    public void ActivateP1AbilityUI(string Ability) {
        DeactivateP1AbilityUI();

        if (Ability == "Barrier") {
            BarrierP1.SetActive(true);
        }

        if (Ability == "SwapHealth") {
            SwapHealthP1.SetActive(true);
        }

        if (Ability == "DoubleDamage") {
            DoubleDamageP1.SetActive(true);
        }

        if (Ability == "Vampirism") {
            VampirismP1.SetActive(true);
        }
    }

    public void DeactivateP1AbilityUI() {
        BarrierP1.SetActive(false);

        SwapHealthP1.SetActive(false);   

        DoubleDamageP1.SetActive(false); 

        VampirismP1.SetActive(false);
    }

    public void ActivateP2AbilityUI(string Ability) {
        DeactivateP2AbilityUI();

        if (Ability == "Barrier") {
            BarrierP2.SetActive(true);
        }

        if (Ability == "SwapHealth") {
            SwapHealthP2.SetActive(true);
        }

        if (Ability == "DoubleDamage") {
            DoubleDamageP2.SetActive(true);
        }

        if (Ability == "Vampirism") {
            VampirismP2.SetActive(true);
        }
    }

    public void DeactivateP2AbilityUI() {
        BarrierP2.SetActive(false);

        SwapHealthP2.SetActive(false);   

        DoubleDamageP2.SetActive(false); 

        VampirismP2.SetActive(false);
    }

    IEnumerator HitIndicator(GameObject Object) {
        Object.SetActive(true);

        yield return new WaitForSeconds(HitDuration);

        Object.SetActive(false);
    }

    public void TogglePauseMenu(bool IsPaused) {
        PauseMenuUI.SetActive(IsPaused);

        if (IsPaused) {
            EventSystem.current.SetSelectedGameObject(PauseMenuFirstButton);
        } 
        
        else {
            EventSystem.current.SetSelectedGameObject(NonLabelObject);
        }
    }
}
