using System.Collections.Generic;
using UnityEngine;

public class TrashCleanUp : MonoBehaviour
{
    public List<GameObject> trashList = new List<GameObject>();
    public string fireballTag = "Fireball";
    public bool trashCleaned = false;
    public bool trashDestroyed = false;
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;
    public GameObject FireToActivate;
    public LayerMask defaultLayer;

    private List<GameObject> trashItemsInCollider = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fireballTag))
        {
            if (AllTrashItemsInCollider())
            {
                trashDestroyed = true;
                objectToDeactivate.SetActive(false);
                objectToActivate.SetActive(true);
                FireToActivate.SetActive(true);
            }
        }
        else if (trashList.Contains(other.gameObject))
        {
            trashItemsInCollider.Add(other.gameObject);
            trashCleaned = (trashItemsInCollider.Count == trashList.Count);
            other.gameObject.layer = defaultLayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (trashList.Contains(other.gameObject))
        {
            trashItemsInCollider.Remove(other.gameObject);
            trashCleaned = false;
        }
    }

    private bool AllTrashItemsInCollider()
    {
        foreach (GameObject trashItem in trashList)
        {
            if (!trashItemsInCollider.Contains(trashItem))
                return false;
        }

        return trashItemsInCollider.Count == trashList.Count;
    }
}
