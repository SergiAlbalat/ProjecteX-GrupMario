using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int maxBounces;
    public Player player;
    private int numBounces = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Destroy(collision.gameObject); // enemy dies
            player.stack.Push(gameObject);
            gameObject.SetActive(false);
            numBounces = 0;
        }
        numBounces++;
        if(numBounces >= maxBounces)
        {
            player.stack.Push(gameObject);
            gameObject.SetActive(false);
            numBounces = 0;
        }
    }
}
