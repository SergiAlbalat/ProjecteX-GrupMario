using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private GameObject breakableFloor;
    public void ActivateAxe()
    {
        breakableFloor.SetActive(false);
    }
}
