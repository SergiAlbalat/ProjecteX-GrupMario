using UnityEngine;
[RequireComponent (typeof(FloatBehaviour))]

public class PiranaPlant : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5;
    private FloatBehaviour _fB;
    private bool _emerged = false;
    private float _targetPosition;
    private void Awake()
    {
        _fB = GetComponent<FloatBehaviour>();
        _targetPosition = transform.position.y;
    }
    private void Start()
    {
        InvokeRepeating("AppearDissapear", 5, 5);
    }
    private void Update()
    {
        if(_emerged && transform.position.y <= _targetPosition)
        {
            _fB.FloatTo(Vector3.up);
        }else if(!_emerged && transform.position.y >= _targetPosition)
        {
            _fB.FloatTo(Vector3.down);
        }
    }
    private void AppearDissapear()
    {
        if (_emerged)
        {
            _emerged = false;
            _targetPosition -= moveDistance;
        }
        else
        {
            _emerged = true;
            _targetPosition += moveDistance;
        }
    }
}
