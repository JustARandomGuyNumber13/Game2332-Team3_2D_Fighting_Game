using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UI_KeyBinding_AxisType : MonoBehaviour
{
    [SerializeField] private GameObject _keyBindingProcessImage;
    [SerializeField] private TMP_Text _keyText;
    [SerializeField] private Button _keyButton;
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private string _actionMapName;
    [SerializeField] private string _keyBindingActionName;
    [SerializeField] private bool _isNegativeKey; // Added: Specify positive or negative

    private int _bindingIndex;
    private InputActionMap _actionMap;
    private InputAction _action;

    private void Start()
    {
        _actionMap = _actionAsset.FindActionMap(_actionMapName);
        if (_actionMap == null)
        {
            Debug.LogError("Input Action Map not exist in this Input Action Asset. \'" + _actionMapName + "\' from " + gameObject.name);
            Destroy(gameObject);
            return; // End frame immediately
        }

        _action = _actionMap.FindAction(_keyBindingActionName);
        if (_action == null)
        {
            Debug.LogError("Input Action not exist in this Input Action Map. \'" + _keyBindingActionName + "\' from " + gameObject.name);
            Destroy(gameObject);
            return; // End frame immediately
        }

        if (!_action.bindings[0].isComposite)
        {
            Debug.LogError("This Action is not an Axis type");
            Destroy(gameObject);
            return; // End frame immediately
        }

        _bindingIndex = FindBindingIndex();
        if (_bindingIndex == -1)
        {
            Debug.LogError("Action " + _keyBindingActionName + " doesn't contain this binding index. Key length: " + _action.bindings.Count + ",current index: " + _bindingIndex);
            Destroy(gameObject);
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
        _action.Disable();
        _action.PerformInteractiveRebinding(_bindingIndex) // Added: Use the binding index
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
    private int FindBindingIndex()
    {
        for (int i = 0; i < _action.bindings.Count; i++)
        {
            if (_action.bindings[i].isPartOfComposite)
            {
                if (!_isNegativeKey && _action.bindings[i].name == "positive")
                {
                    return i;
                }
                else if (_isNegativeKey && _action.bindings[i].name == "negative")
                {
                    return i;
                }
            }
        }
        return -1; // Not found
    }
    private void UpdateKeyText()
    {
        _keyText.text = _action.GetBindingDisplayString(_bindingIndex);
    }
}
