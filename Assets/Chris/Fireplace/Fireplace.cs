using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    public List<GameObject> firewoodObjects = new List<GameObject>();
    public List<GameObject> fireplaceLocations = new List<GameObject>();
    public GameObject fireballEffect;
    public GameObject IncompleteCandles;
    public GameObject CompleteCandles;

    private bool fireWoodReady = false;
    private bool FirePlaceComplete = false;
    private int nextFirewoodIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (firewoodObjects.Contains(other.gameObject))
        {
            int index = firewoodObjects.IndexOf(other.gameObject);
            GameObject firewood = firewoodObjects[index];
            GameObject fireplaceLocation = fireplaceLocations[nextFirewoodIndex];

            firewood.transform.position = fireplaceLocation.transform.position;
            firewood.transform.rotation = fireplaceLocation.transform.rotation;
            firewood.GetComponent<Rigidbody>().isKinematic = true;

            // Change the layer of the firewood object to default
            firewood.layer = LayerMask.NameToLayer("Default");

            firewoodObjects[index] = null;
            nextFirewoodIndex++;

            CheckFireWoodReady();
        }
        else if (other.CompareTag("Fireball") && fireWoodReady)
        {
            fireballEffect.SetActive(true);
            IncompleteCandles.SetActive(false);
            CompleteCandles.SetActive(true);
            FirePlaceComplete = true;
        }
    }

    private void CheckFireWoodReady()
    {
        foreach (GameObject firewood in firewoodObjects)
        {
            if (firewood != null)
            {
                return;
            }
        }

        fireWoodReady = true;
        // Perform any desired actions when all firewood objects are in place
    }
}
