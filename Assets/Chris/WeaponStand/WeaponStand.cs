using System.Collections.Generic;
using UnityEngine;

public class WeaponStand : MonoBehaviour
{
    public List<GameObject> weaponObjects = new List<GameObject>();
    public List<GameObject> standLocations = new List<GameObject>();
    public GameObject weaponStackEffect;
    public GameObject IncompleteTorches;
    public GameObject CompleteTorches;

    private int filledStandCount = 0;
    private bool WeaponStandComplete = false;
    private int nextWeaponIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (weaponObjects.Contains(other.gameObject))
        {
            int index = weaponObjects.IndexOf(other.gameObject);

            if (index < standLocations.Count)
            {
                GameObject weapon = weaponObjects[index];
                GameObject standLocation = standLocations[index];

                weapon.transform.position = standLocation.transform.position;
                weapon.transform.rotation = standLocation.transform.rotation;
                weapon.GetComponent<Rigidbody>().isKinematic = true;

                weapon.GetComponent<Collider>().enabled = false;

                weaponObjects[index] = null;
                filledStandCount++;

                CheckWeaponsReady();
            }
        }
        else if (WeaponStandComplete)
        {
            //weaponStackEffect.SetActive(true);
            //IncompleteTorches.SetActive(false);
            //CompleteTorches.SetActive(true);
        }
    }

    private void CheckWeaponsReady()
    {
        int placedWeaponCount = 0;

        foreach (GameObject weapon in weaponObjects)
        {
            if (weapon == null)
            {
                placedWeaponCount++;
            }
        }

        if (placedWeaponCount == standLocations.Count)
        {
            WeaponStandComplete = true;
            // Perform any desired actions when all weapons are placed on the stand
            IncompleteTorches.SetActive(false);
            CompleteTorches.SetActive(true);
        }
    }
}
