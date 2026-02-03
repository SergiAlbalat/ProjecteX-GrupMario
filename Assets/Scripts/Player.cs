using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(PlayerCamera))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileVelocity = 3f;
    private MoveBehaviour _mB;
    private InputSystem_Actions _inputActions;
    private Vector2 direction;
    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
    private void FixedUpdate()
    {
        _mB.MoveFirstPerson(new Vector3(direction.x, 0, direction.y));
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mB.Jump();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You death =(");
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
            _mB.Run(true);
        if (context.canceled)
            _mB.Run(false);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
            proj.GetComponent<Rigidbody>().linearVelocity = transform.forward * projectileVelocity * Time.deltaTime;
        }
    }
}