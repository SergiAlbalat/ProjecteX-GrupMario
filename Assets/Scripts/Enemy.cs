using UnityEngine;
[RequireComponent(typeof(MoveBehaviour))]
public class Enemy : MonoBehaviour
{
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
        _mB.MoveTo(currentTarget.position);

        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        Vector3 objectivePosition = currentTarget.position;
        objectivePosition.y = 0;
        if (Vector3.Distance(currentPosition, objectivePosition) < 0.1f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        currentTarget = currentTarget == pos1 ? pos2 : pos1;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("MarioFireball"))
        {
            Debug.Log("aaaaa");
            Destroy(gameObject);
            //DropCoin
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillFoot"))
        {
            Destroy(gameObject);
        }
    }
}