using UnityEngine;
[RequireComponent (typeof(CharacterController))]

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    private Vector3 _velocity;
    [SerializeField] private float characterSpeed = 10;
    [SerializeField] private float runSpeed = 15;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float gravity = 0.2f;
    [SerializeField] private float jumpForce = 0.1f;
    private float currentSpeed; 
    private void Awake()
    {   
        _cC = GetComponent<CharacterController>();
        currentSpeed = characterSpeed;
    }
    private void FixedUpdate()
    {
        if (_cC.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.01f;
        }
        _velocity.y -= gravity * Time.deltaTime;
        _cC.Move(_velocity);
    }
    public void MoveFirstPerson(Vector3 direction)
    {
        Vector3 movement = direction.x * transform.right + direction.z * transform.forward;
        Move(movement);
    }
    public void Jump()
    {
        if(_cC.isGrounded)
        {
            _velocity.y = jumpForce;
        }
    }
    public void Rotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
    public void MoveTo(Vector3 position)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        Vector3 objectivePosition = position;
        objectivePosition.y = 0;
        Vector3 direction = (objectivePosition - currentPosition).normalized;
        Rotate(direction);
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(forward, direction);
        if(angle < 5)
        {
            Move(forward);
        }
    }
    public void MoveAway(Vector3 position)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        Vector3 objectivePosition = position;
        objectivePosition.y = 0;
        Vector3 direction = (objectivePosition - currentPosition).normalized;
        Rotate(direction);
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(forward, direction);
        if (angle < 5)
        {
            Move(-forward);
        }
    }
    public void Move(Vector3 direction)
    {
        _cC.Move(direction * currentSpeed * Time.deltaTime);
    }
    public void Run(bool activated)
    {
        if (activated)
            currentSpeed = runSpeed;
        else
            currentSpeed = characterSpeed;
    }
    public void Bounce()
    {
        _velocity.y = jumpForce/2;
    }
    public void Teleport(Vector3 position)
    {
        _cC.enabled = false;
        transform.position = position;
        _cC.enabled = true;
    }
}