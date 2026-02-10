using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof(MoveBehaviour))]

public class Shell : MonoBehaviour
{
    private MoveBehaviour _mB;
    private Vector3 _direction = new Vector3(0, 0, 0);
    public bool moving = false;
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
}