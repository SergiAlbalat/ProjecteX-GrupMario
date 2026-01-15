using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour, InputSystem_Actions.ICameraActions
{
    private float mouseX, mouseY, rotation;

    public void OnMoveHorizontally(InputAction.CallbackContext context)
    {
        mouseX = context.ReadValue<float>();
    }
    public void OnMoveVertically(InputAction.CallbackContext context)
    {
        mouseY = context.ReadValue<float>();
    }
    private void Update()
    {
        
    }
}
