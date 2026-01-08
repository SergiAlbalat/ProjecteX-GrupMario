using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    public void Move(Vector3 direction)
    {
        _cC.Move(direction);
    }
}
