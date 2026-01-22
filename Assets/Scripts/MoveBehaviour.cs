using UnityEngine;
[RequireComponent (typeof(CharacterController))]

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    private Vector3 _velocity;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float playerSpeed = 5;
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
        Vector3 cameraForward = cameraPosition.forward;
        cameraForward.y = 0;
        Quaternion rotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = rotation;
    }
    public void Move(Vector3 direction)
    {
        Vector3 movement = direction.x * transform.right + direction.z * transform.forward;
        _cC.Move(movement * playerSpeed * Time.deltaTime);
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
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
