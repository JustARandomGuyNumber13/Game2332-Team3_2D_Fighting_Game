using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionUINavigator : MonoBehaviour
{
    [SerializeField]
    private GameObject UIManager;

    public InputActionReference moveRight;
    public InputActionReference moveLeft;
    public InputActionReference confirm;
    public InputActionReference goBack;

    private void OnEnable()
    {
        SelectionUIManager selectionUIManager = UIManager.GetComponent<SelectionUIManager>();

        moveRight.action.started += selectionUIManager.MoveRight;
        moveLeft.action.started += selectionUIManager.MoveLeft;
        confirm.action.started += selectionUIManager.Confirm;
        goBack.action.started += selectionUIManager.GoBack;
    }
    
}
