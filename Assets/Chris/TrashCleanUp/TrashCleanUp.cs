using System.Collections.Generic;
using UnityEngine;

public class TrashCleanUp : MonoBehaviour
{
    public List<GameObject> trashList = new List<GameObject>();
    public bool trashCleaned = false;
    public bool trashDestroyed = false;
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

    private List<GameObject> trashItemsInCollider = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireball"))
        {
            foreach (GameObject trashItem in trashItemsInCollider)
            {
                if (trashItem.activeSelf)
                    trashItem.SetActive(false);
            }

            if (AllTrashItemsInactive())
            {
                trashDestroyed = true;
                objectToDeactivate.SetActive(false);
                objectToActivate.SetActive(true);
            }
        }
        else if (trashList.Contains(other.gameObject))
        {
            trashItemsInCollider.Add(other.gameObject);
            trashCleaned = (trashItemsInCollider.Count == trashList.Count);
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

    private bool AllTrashItemsInactive()
    {
        foreach (GameObject trashItem in trashList)
        {
            if (trashItem.activeSelf)
                return false;
        }

        return true;
    }
}
