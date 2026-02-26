using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockType blockType;
    [SerializeField] private GameObject itemInside;
    [SerializeField] private Player player;
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
        Quaternion itemRotation = Quaternion.LookRotation(player.transform.position);
        itemRotation.y = 0;
        Instantiate(itemInside, transform.position, itemRotation);
    }
}
