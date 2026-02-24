using UnityEngine;
[RequireComponent (typeof(FloatBehaviour))]

public class BowserProjectile : MonoBehaviour
{
    private FloatBehaviour _fB;
    public Bowser bowser;
    [SerializeField] private float lifeTime = 10;
    private void Awake()
    {
        _fB = GetComponent<FloatBehaviour>();
        Invoke("ReturnToStack", lifeTime);
    }
    private void FixedUpdate()
    {
        _fB.FloatForward();
    }
    private void ReturnToStack()
    {
        bowser.bowserProjectiles.Push(gameObject);
        gameObject.SetActive(false);
    }
}
