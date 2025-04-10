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
    public InputActionReference ready;

    private void OnEnable()
    {
        SelectionUI selectionUI = UIManager.GetComponent<SelectionUI>();

        moveRight.action.Enable();
        moveLeft.action.Enable();
        confirm.action.Enable();
        goBack.action.Enable();
        ready.action.Enable();


        moveRight.action.started += selectionUI.MoveRight;
        moveLeft.action.started += selectionUI.MoveLeft;
        confirm.action.started += selectionUI.Confirm;
        goBack.action.started += selectionUI.GoBack;
        ready.action.started += selectionUI.Ready;
    }
    
}
