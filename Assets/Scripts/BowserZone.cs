using UnityEngine;

public class BowserZone : MonoBehaviour
{
    private Vector3 _bowserSecuredPosition;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bowser"))
        {
            _bowserSecuredPosition = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bowser"))
        {
            MoveBehaviour bowserMB = other.GetComponent<MoveBehaviour>();
            bowserMB.Teleport(_bowserSecuredPosition);
        }
    }
}