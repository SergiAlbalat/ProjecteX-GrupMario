using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(MoveBehaviour))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> keyPositions;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GameObject shell;
    private MoveBehaviour _mB;
    private Transform currentTarget;
    private int currentKeyPosition = 0;

    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        currentTarget = keyPositions[0];
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
        currentKeyPosition += 1;
        if(currentKeyPosition == keyPositions.Count)
        {
            currentKeyPosition = 0;
            keyPositions.Reverse();
        }
        currentTarget = keyPositions[currentKeyPosition];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillFoot"))
        {
            Die();
        }
    }
    private void Die()
    {
        switch(enemyType)
        {
            case EnemyType.Goomba:
                Destroy(gameObject);
                break;
            case EnemyType.Koopa:
                IntoShell();
                break;
        }
    }
    private void IntoShell()
    {
        Instantiate(shell, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}