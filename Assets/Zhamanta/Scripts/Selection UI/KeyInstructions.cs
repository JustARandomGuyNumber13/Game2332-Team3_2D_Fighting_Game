using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class KeyInstructions : MonoBehaviour
{
    [SerializeField] InputActionAsset _p1ActionAsset;
    [SerializeField] InputActionAsset _p2ActionAsset;

    [Header("Player 1")]
    [SerializeField] private TMP_Text _p1ConfirmText;
    [SerializeField] private TMP_Text _p1ReadyText;
    [SerializeField] private TMP_Text _p1BackText;

    [Header("Player 2")]
    [SerializeField] private TMP_Text _p2ConfirmText;
    [SerializeField] private TMP_Text _p2ReadyText;
    [SerializeField] private TMP_Text _p2BackText;

    private void Start()
    {
        InputActionMap _p1InputMap = _p1ActionAsset.FindActionMap("Player");
        InputActionMap _p2InputMap = _p2ActionAsset.FindActionMap("Player");

        SetUpInstruction(_p1ConfirmText, _p1ReadyText, _p1BackText, _p1InputMap);
        SetUpInstruction(_p2ConfirmText, _p2ReadyText, _p2BackText, _p2InputMap);
    }

    private void SetUpInstruction(TMP_Text confirmText, TMP_Text readyText, TMP_Text backText, InputActionMap map)
    {
        confirmText.text += "Confirm: " + map.FindAction("SkillOne").GetBindingDisplayString();
        backText.text += "Back: " + map.FindAction("SkillTwo").GetBindingDisplayString();
        readyText.text += "Ready: " + map.FindAction("SkillThree").GetBindingDisplayString();
    }
}
