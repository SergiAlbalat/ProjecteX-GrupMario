using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(MoveBehaviour))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private MoveBehaviour _mB;
    private InputSystem_Actions _inputActions;
    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        _inputActions = new InputSystem_Actions();
    }
    private void FixedUpdate()
    {
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
