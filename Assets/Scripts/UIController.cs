using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour, InputSystem_Actions.IUIActions
{
    private InputSystem_Actions _inputActions;
    private NavigationBehaviour _navigationBehaviour;
    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.UI.SetCallbacks(this);
        _navigationBehaviour = GetComponent<NavigationBehaviour>();
    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
    public void OnExit(InputAction.CallbackContext context)
    {
        _navigationBehaviour.ExitGame();
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        _navigationBehaviour.StartGame();
    }
}
