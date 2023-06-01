using System.Collections;
using UnityEngine;

public class FireballWand : MonoBehaviour
{
    public GameObject fireballPrefab;        // The fireball prefab to be instantiated
    public Transform fireballSpawnPoint;     // The point at which the fireball will spawn
    public float fireballSpeed = 20f;        // The speed at which the fireball will travel
    public float interactRadius = 0.5f;      // The radius to check for objects in contact with the wand

    public GameObject leftHandDetection;     // The GameObject representing the left hand detection point
    public GameObject rightHandDetection;    // The GameObject representing the right hand detection point

    private bool canShoot = true;            // Add a boolean flag to indicate if the player can shoot a fireball
    private float fireballCooldown = 0.5f;   // Add a float to set the cooldown time in seconds

    public OVRInput.Button spellButtonL;
    public OVRInput.Button spellButtonR;

    void Update()
    {
        // Check if the left index trigger is pressed while the controller is in contact with the left hand detection point
        if (OVRInput.GetDown(spellButtonL) && IsColliding(leftHandDetection))
        {
            ShootFireball();
        }

        // Check if the right index trigger is pressed while the controller is in contact with the right hand detection point
        if (OVRInput.GetDown(spellButtonR) && IsColliding(rightHandDetection))
        {
            ShootFireball();
        }
    }

    bool IsColliding(GameObject detectionPoint)
    {
        // Check if the detection point's collider is colliding with any other colliders
        Collider[] colliders = detectionPoint.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.bounds.Intersects(GetComponent<Collider>().bounds))
            {
                return true;
            }
        }
        return false;
    }

    void ShootFireball()
    {
        if (canShoot)
        {
            // Get the forward direction of the fireball spawn point
            Vector3 fireballDirection = fireballSpawnPoint.forward;

            // Instantiate the fireball at the spawn point, with the desired scale
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
            fireball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.fireSpellSFX);

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
