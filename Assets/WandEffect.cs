using UnityEngine;

public class WandEffect : MonoBehaviour
{
    public GameObject objectToTurnOff;
    public BoxCollider triggerCollider;
    public GameObject wand;


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == wand)
        {
            objectToTurnOff.SetActive(false);
        }
    }
}
