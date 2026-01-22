using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float rotationSpeed = 5f;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
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
