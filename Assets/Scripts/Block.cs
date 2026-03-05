using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockType blockType;
    [SerializeField] private GameObject itemInside;
    [SerializeField] private Transform player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractHead"))
        {
            OnInteract();
        }
    }
    private void OnInteract()
    {   
        switch (blockType)
        {
            case BlockType.BreakableBricks:
                Destroy(gameObject);
                break;
            case BlockType.QuestionBlock:
                SpawnItem();
                break;
            case BlockType.UnbreakableBricks:
                //Get coin
                break;
        }
    }
    private void SpawnItem()
    {
        Quaternion itemRotation = Quaternion.LookRotation(-player.transform.position);
        itemRotation.x = 0;
        itemRotation.z = 0;
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Instantiate(itemInside, transform.position + offset, itemRotation);
    }
    
}
