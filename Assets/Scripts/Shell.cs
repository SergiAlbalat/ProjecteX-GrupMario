using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Shell : MonoBehaviour
{
    [SerializeField] private float bounceCooldown = 0.1f;
    [SerializeField] private float shellVelocity = 10f;
    private Rigidbody _rB;
    private Vector3 _direction = new Vector3(0, 0, 0);
    public bool moving = false;
    private float _lastBounceTime = -10f;
    private void Awake()
    {
        _rB = GetComponent<Rigidbody>();
    }
    public void GetDirection(Vector3 direction, Quaternion rotation)
    {
        _direction = direction;
        transform.rotation = rotation;
    }
    private void Update()
    {
        _rB.linearVelocity = _direction * shellVelocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - _lastBounceTime < bounceCooldown)
            return;
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Vector3 normal = collision.contacts[0].normal;
            _direction = Vector3.Reflect(_direction, normal).normalized;
            _lastBounceTime = Time.time;
        }
    }
}