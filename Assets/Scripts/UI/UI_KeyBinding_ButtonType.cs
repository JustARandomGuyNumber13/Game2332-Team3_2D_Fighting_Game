using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UI_KeyBinding_ButtonType : MonoBehaviour
{
    [SerializeField] private GameObject _keyBindingProcessImage;
    [SerializeField] private TMP_Text _keyText;
    [SerializeField] private Button _keyButton;
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private string _keyBindingActionName;

    private InputAction _action;

    private void Start()
    {
        _action = _actionAsset.FindActionMap("Player").FindAction(_keyBindingActionName);
        if (_action == null)
        {
            Debug.LogError("Input Action not exist in this Input Action Map. \'" + _keyBindingActionName + "\' from " + gameObject.name);
            return; // End frame immediately
        }

        _keyButton.onClick.AddListener(ToggleRebindKey);
        UpdateKeyText();
    }

    private void ToggleRebindKey()
    {
        _keyBindingProcessImage.SetActive(true);
        RebindKey();
    }

    private void RebindKey()
    {
        _action.Disable(); // Disable the action to rebind
        _action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                operation.Dispose();
                _action.Enable();
                UpdateKeyText();
               _keyBindingProcessImage.SetActive(false);
            })
            .Start();
    }
    private void UpdateKeyText()
    {
        _keyText.text = _action.GetBindingDisplayString(); // Update the text
    }
}
