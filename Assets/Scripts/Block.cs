using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockType blockType;
    [SerializeField] private GameObject itemInside;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject emptyBlockPrefab;
    private bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractHead"))
        {
            OnInteract();
        }
    }
    private void OnInteract()
    {   
        if (!collected) {
            switch (blockType)
            {
                case BlockType.BreakableBricks:
                    GameManager.gm.PlayAudio(SoundManager.AudioClips.Brick);
                    Destroy(gameObject);
                    break;
                case BlockType.QuestionBlock:
                    SpawnItem();
                    GameManager.gm.PlayAudio(SoundManager.AudioClips.BlockItem);
                    collected = true;
                    Instantiate(emptyBlockPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    break;
                case BlockType.CoinBlocks:
                    GameManager.gm.GotCoin();
                    collected = true;
                    Instantiate(emptyBlockPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    break;
            }
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
