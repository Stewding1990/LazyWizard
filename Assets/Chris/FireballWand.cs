using System.Collections;
using UnityEngine;

public class FireballWand : MonoBehaviour
{
    public GameObject fireballPrefab;        // The fireball prefab to be instantiated
    public Transform fireballSpawnPoint;     // The point at which the fireball will spawn
    public float fireballSpeed = 20f;        // The speed at which the fireball will travel

    private bool canShoot = true;            // Add a boolean flag to indicate if the player can shoot a fireball
    private float fireballCooldown = 0.5f;   // Add a float to set the cooldown time in seconds

    void Update()
    {
        // Check if the index trigger on either Oculus Quest 2 controller is pressed
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        if (canShoot)
        {
            // Get the forward direction of the fireball spawn point
            Vector3 fireballDirection = fireballSpawnPoint.forward;

            // Instantiate the fireball at the spawn point, with the desired scale
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
            //fireball.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

            // Add physics to the fireball
            Rigidbody fireballRigidbody = fireball.GetComponent<Rigidbody>();
            fireballRigidbody.velocity = fireballDirection * fireballSpeed;

            // Destroy the fireball after a set amount of time
            Destroy(fireball, 3f);

            // Set the canShoot flag to false, preventing the player from shooting another fireball
            canShoot = false;

            // Start a coroutine to reset the canShoot flag after the cooldown period has elapsed
            StartCoroutine(ResetShootCooldown());
        }
    }

    IEnumerator ResetShootCooldown()
    {
        // Wait for the cooldown period to elapse
        yield return new WaitForSeconds(fireballCooldown);

        // Set the canShoot flag to true, allowing the player to shoot another fireball
        canShoot = true;
    }
}
