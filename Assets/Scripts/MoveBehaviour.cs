using UnityEngine;
[RequireComponent (typeof(CharacterController))]

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    private Vector3 _velocity;
    [SerializeField] private float characterSpeed = 5;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float gravity = 0.2f;
    [SerializeField] private float jumpForce = 0.1f;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (_cC.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1;
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
        //forward.y = 0;
        //forward.Normalize();
        float angle = Vector3.Angle(forward, direction);
        if(angle < 5)
        {
            Move(forward);
        }
        //Hacer un move para npc propio
    }
    public void Move(Vector3 direction)
    {
        _cC.Move(direction * characterSpeed * Time.deltaTime);
    }
}
