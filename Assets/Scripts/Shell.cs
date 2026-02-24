using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof(MoveBehaviour))]

public class Shell : MonoBehaviour
{
    [SerializeField] private float bounceCooldown = 0.1f;
    private MoveBehaviour _mB;
    private Vector3 _direction = new Vector3(0, 0, 0);
    public bool moving = false;
    private float _lastBounceTime = -10f;
    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
    }
    public void GetDirection(Vector3 direction, Quaternion rotation)
    {
        _direction = direction;
        transform.rotation = rotation;
    }
    private void Update()
    {
        _mB.MoveFirstPerson(_direction);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(Time.time - _lastBounceTime < bounceCooldown)
            return;
        if (hit.gameObject.CompareTag("Terrain"))
        {
            Vector3 normal = hit.normal;
            // Vector3 dirHorizontal = new Vector3(_direction.x, 0f, _direction.z);
            // if (Vector3.Dot(dirHorizontal, normal) >= 0f) return;
            _direction = Vector3.Reflect(_direction, normal).normalized;
            _lastBounceTime = Time.time;
        }
    }
}