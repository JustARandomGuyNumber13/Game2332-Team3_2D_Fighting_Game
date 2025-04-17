using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class KeyInstructions : MonoBehaviour
{
    [SerializeField] InputActionAsset _actionAsset;

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
        if (!_actionAsset.name.Equals("FightingPlayer"))
        {
            Debug.LogError("Only \"FightingPlayer\" Input Action Asset is allow ");
            return;
        }

        InputActionMap _p1InputMap = _actionAsset.FindActionMap("Player1");
        InputActionMap _p2InputMap = _actionAsset.FindActionMap("Player2");

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
