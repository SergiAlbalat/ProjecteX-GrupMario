using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class FloatBehaviour : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 10;
    private CharacterController _cC;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    public void FloatTo(Vector3 direction)
    {
        _cC.Move(direction * floatSpeed * Time.deltaTime);
    }
    public void FloatForward()
    {
        Vector3 movement = new Vector3(0, 0, 1);
        Vector3 direction = movement.x * transform.right + movement.z * transform.forward;
        _cC.Move(direction * floatSpeed * Time.deltaTime);
    }
}
