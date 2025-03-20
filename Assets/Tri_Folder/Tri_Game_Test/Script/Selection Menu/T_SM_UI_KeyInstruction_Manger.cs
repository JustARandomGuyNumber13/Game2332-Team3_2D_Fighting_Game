using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class T_SM_UI_KeyInstruction_Manger : MonoBehaviour
{
    [SerializeField] InputActionAsset _actionAsset;

    [Header("Player 1")]
    [SerializeField] private TMP_Text _p1ChooseText;
    [SerializeField] private TMP_Text _p1ReadyText;

    [Header("Player 2")]
    [SerializeField] private TMP_Text _p2ChooseText;
    [SerializeField] private TMP_Text _p2ReadyText;

    private void Start()
    {
        if (!_actionAsset.name.Equals("FightingPlayer"))
        {
            Debug.LogError("Only \"FightingPlayer\" Input Action Asset is allow ");
            return;
        }

        InputActionMap _p1InputMap = _actionAsset.FindActionMap("Player1");
        InputActionMap _p2InputMap = _actionAsset.FindActionMap("Player2");

        SetUpInstruction(_p1ChooseText, _p1ReadyText, _p1InputMap);
        SetUpInstruction(_p2ChooseText, _p2ReadyText, _p2InputMap);
    }

    private void SetUpInstruction(TMP_Text chooseText, TMP_Text readyText, InputActionMap map)
    {
        chooseText.text += map.FindAction("Attack").GetBindingDisplayString();
        readyText.text += map.FindAction("SkillOne").GetBindingDisplayString();
    }
}
