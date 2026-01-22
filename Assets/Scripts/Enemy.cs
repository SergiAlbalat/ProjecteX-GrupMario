using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    private MoveBehaviour _mB;
    private Transform currentTarget;

    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        currentTarget = pos2;
    }

    private void Update()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        _mB.Move(direction * moveSpeed * Time.deltaTime);
        _mB.Rotate(direction);

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        currentTarget = currentTarget == pos1 ? pos2 : pos1;
    }
}