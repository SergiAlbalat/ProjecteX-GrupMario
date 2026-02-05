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
}
