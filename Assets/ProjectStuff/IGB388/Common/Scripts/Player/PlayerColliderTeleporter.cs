using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTeleporter : MonoBehaviour
{
    private Transform playerTransform;
    private Collider playerCollider;
    private Vector3 colliderOffset;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        playerTransform = transform; // Assign the player's transform
        playerCollider = GetComponent<Collider>(); // Get the collider component

        colliderOffset = playerCollider.bounds.center - playerTransform.position; // Calculate the initial offset between the collider and the player
        lastPlayerPosition = playerTransform.position; // Store the initial player position
    }

    private void Update()
    {
        if (playerTransform.position != lastPlayerPosition)
        {
            UpdateColliderPosition();
            lastPlayerPosition = playerTransform.position;
        }
    }

    private void UpdateColliderPosition()
    {
        playerCollider.transform.position = playerTransform.position + colliderOffset; // Update the collider's position
    }
}
