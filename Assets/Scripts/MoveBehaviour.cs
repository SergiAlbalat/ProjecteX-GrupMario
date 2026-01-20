using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float steerSpeed = 1;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        Vector3 cameraForward = cameraPosition.forward;
        cameraForward.y = 0;
        Quaternion rotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, steerSpeed * Time.deltaTime);
    }
    public void Move(Vector3 direction)
    {
        Vector3 movement = transform.forward * direction.z + transform.right * direction.x;
        _cC.Move(direction);
    }
}
