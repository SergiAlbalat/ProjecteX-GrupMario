using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    private Vector3 _velocity;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float gravity = 0.2f;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        if (_cC.isGrounded)
        {
            _velocity.y = -2;
        }
        else
        {
            _velocity.y += -gravity;
        }
        _cC.Move(_velocity * Time.deltaTime);
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
