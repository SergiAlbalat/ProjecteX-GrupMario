using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    private void Update()
    {
        Vector3 cameraForward = cameraPosition.forward;
        cameraForward.y = 0;
        Quaternion rotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = rotation;
    }
}
