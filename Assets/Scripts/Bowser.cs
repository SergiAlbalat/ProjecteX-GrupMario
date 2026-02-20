using UnityEngine;
[RequireComponent(typeof(MoveBehaviour))]
public class Bowser : MonoBehaviour
{
    private MoveBehaviour _mB;
    private bool _following = false;
    private bool _inRange = false;
    private Vector3 _playerPosition;
    private Vector3 _containedPosition;
    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        InvokeRepeating("Jump", 4, 4);
        InvokeRepeating("ChangeMovement", 3, 3);
    }
    private void Jump()
    {
        _mB.Jump();
    }
    private void ChangeMovement()
    {
        _following = !_following;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        _inRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;
        _inRange = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        _playerPosition = other.gameObject.transform.position;
    }
    private void Update()
    {
        
        if (_following && _inRange)
        {
            _mB.MoveTo(_playerPosition);
        }else if(!_following && _inRange)
        {
            _mB.MoveAway(_playerPosition);
        }
    }
}
