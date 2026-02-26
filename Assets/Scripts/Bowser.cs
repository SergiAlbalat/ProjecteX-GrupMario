using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveBehaviour))]
public class Bowser : MonoBehaviour
{
    public Stack<GameObject> bowserProjectiles = new Stack<GameObject>();
    private MoveBehaviour _mB;
    private bool _following = false;
    private bool _inRange = false;
    private Vector3 _playerPosition;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint;
    private void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        InvokeRepeating("Jump", 4, 4);
        InvokeRepeating("ChangeMovement", 3, 3);
        InvokeRepeating("ShootFireball", 3.5f, 3.5f);
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
        if (other.gameObject.CompareTag("Player"))
        {
            _inRange = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
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
    private void ShootFireball()
    {
        if (bowserProjectiles.Count == 0)
        {
            GameObject proj = Instantiate(projectile, shootPoint.position, transform.rotation);
            proj.GetComponent<BowserProjectile>().bowser = this;
        }
        else
        {
            GameObject proj = bowserProjectiles.Pop();
            proj.transform.position = shootPoint.position;
            proj.transform.rotation = transform.rotation;
            proj.SetActive(true);
        }
    }
}
