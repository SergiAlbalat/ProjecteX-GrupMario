using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int maxBounces;
    private int numBounces = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Destroy(collision.gameObject); // enemy dies
            Destroy(gameObject);           // fireball disappears
        }
        numBounces++;
        if(numBounces >= maxBounces)
        {
            Destroy(gameObject);
        }
    }
}
