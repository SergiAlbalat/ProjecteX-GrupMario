using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(PlayerCamera))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileVelocity = 3f;
    [SerializeField] private float immunityFrames = 1f;
    [SerializeField] private Transform shootPoint;
    private CharacterController charController;
    private bool hasFireFlower = false;
    private bool isSmall = true;
    private float lastDmgTime;
    public Stack<GameObject> stack = new Stack<GameObject>();
    private MoveBehaviour _mB;
    private InputSystem_Actions _inputActions;
    private Vector2 direction;
    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);
        charController = GetComponent<CharacterController>();
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
        if (hit.gameObject.CompareTag("Enemy") && Time.time >= lastDmgTime + immunityFrames)
        {
            if (hit.normal.y < 0.5)
            {
                TakeDamage();
            }
        }else if ((hit.gameObject.CompareTag("Bowser") || hit.gameObject.CompareTag("BowserFireball") || hit.gameObject.CompareTag("PiranaPlant")) && Time.time >= lastDmgTime + immunityFrames)
        {
            TakeDamage();
        } else if (hit.gameObject.CompareTag("FireFlower"))
        {
            GrowUp(hit);
            hasFireFlower=true;
        } else if (hit.gameObject.CompareTag("Mushroom"))
        {
            GrowUp(hit);
        }
        if (hit.gameObject.CompareTag("Shell"))
        {
            Shell shell = hit.gameObject.GetComponent<Shell>();
            if (shell.moving)
            {
                TakeDamage();
            }
            else
            {
                shell.GetDirection(transform.localToWorldMatrix.MultiplyVector(new Vector3(direction.x, 0, direction.y)), transform.rotation);
            }
        }
    }
    public void TakeDamage()
    {
        if (isSmall)
        {
            Die();
        }
        else
        {
            hasFireFlower = false;
            isSmall = true;
            charController.enabled = false;
            transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            charController.enabled = true;
        }
        lastDmgTime = Time.time;
    }
    private void GrowUp(ControllerColliderHit powerObject)
    {
        isSmall = false;
        Destroy(powerObject.gameObject);
        charController.enabled = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
        charController.enabled = true;
    }
    public void Die()
    {
        GameManager.LoseLive();
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
        if (context.performed && hasFireFlower)
        {
            if (stack.Count == 0)
            {
                GameObject proj = Instantiate(projectile, shootPoint.position, transform.rotation);
                Rigidbody rb = proj.GetComponent<Rigidbody>();
                rb.linearVelocity = shootPoint.forward * projectileVelocity;
                proj.GetComponent<Projectile>().player = this;
            } else
            {
                GameObject proj = stack.Pop();
                proj.transform.position = shootPoint.position;
                proj.SetActive(true);
                Rigidbody rb = proj.GetComponent<Rigidbody>();
                rb.linearVelocity = shootPoint.forward * projectileVelocity;
            }
        }
    }

    public void JumpOnKill()
    {
        _mB.Bounce();
    }
}