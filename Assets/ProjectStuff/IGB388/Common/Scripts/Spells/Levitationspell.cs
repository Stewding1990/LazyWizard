using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitationspell : MonoBehaviour
{
    [HideInInspector] public CustomGrabbable grabbable;
    [HideInInspector] public Rigidbody rigid;
    public OVRInput.Button spellButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Button distanceControlButton = OVRInput.Button.Two;
    public GameObject wandTip;
    private bool holdingWand = false;
    private bool wandPickedUp = false;  // Add a flag to track if the wand has been picked up

    // Levitation Variables
    private Vector3 initialDistanceToWand;
    private Vector3 currentDistanceToWand;
    private Rigidbody selectedObjectRigidbody;
    public LayerMask levitateLayer;

    // Smoothness variables
    public float smoothing = 40.0f;
    public float distanceChangeSpeed = 1.0f;
    public float minDistanceToWand = 0.5f;
    public float maxDistanceToWand = 20.0f;
    public float distanceThreshold = 0.2f;
    private float maxRaycastDistance = 50f;

    public float VibDuration;
    public float VibStrength;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        grabbable = GetComponent<CustomGrabbable>();
        initialDistanceToWand = wandTip.transform.localPosition * .3f;
        currentDistanceToWand = initialDistanceToWand;
    }

    void FixedUpdate()
    {
        if (holdingWand && OVRInput.Get(spellButton))
        {

            RaycastHit hit;
            if (Physics.Raycast(wandTip.transform.position, wandTip.transform.forward, out hit, maxRaycastDistance, levitateLayer))
            {
                SimpleHapticVibrationManager.VibrateController(VibDuration, VibStrength, OVRInput.Controller.RTouch);

                selectedObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (selectedObjectRigidbody != null)
                {
                    Vector3 targetPosition = CalculateTargetPosition();
                    float distanceToWand = (Vector3.Distance(selectedObjectRigidbody.transform.position, wandTip.transform.position) * .3f);

                    if (OVRInput.Get(distanceControlButton))
                    {
                        if (distanceToWand < maxDistanceToWand)
                        {
                            currentDistanceToWand.z += distanceChangeSpeed * Time.deltaTime;
                            currentDistanceToWand.z = Mathf.Clamp(currentDistanceToWand.z, minDistanceToWand, maxDistanceToWand);
                        }
                    }
                    else
                    {
                        if (distanceToWand > minDistanceToWand)
                        {
                            currentDistanceToWand.z -= distanceChangeSpeed * Time.deltaTime;
                            currentDistanceToWand.z = Mathf.Clamp(currentDistanceToWand.z, minDistanceToWand, maxDistanceToWand);
                        }
                    }

                    selectedObjectRigidbody.useGravity = false;

                    targetPosition = CalculateTargetPosition();
                    MoveObjectTowardsTarget(targetPosition);
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.levitationSFX);

                    Debug.DrawRay(wandTip.transform.position, wandTip.transform.forward * maxRaycastDistance, Color.red);
                }
            }
        }
    
        else
        {
            // Spell button is not pressed, release the object and enable gravity
            ReleaseObject();
            SimpleHapticVibrationManager.VibrateController(0f, 0f, OVRInput.Controller.RTouch);
        }
    }

    void ReleaseObject()
    {
        if (selectedObjectRigidbody != null)
        {
            selectedObjectRigidbody.useGravity = true;
            selectedObjectRigidbody = null;
        }
    }



    Vector3 CalculateTargetPosition()
    {
        return wandTip.transform.position + wandTip.transform.forward * currentDistanceToWand.z;
    }

    void MoveObjectTowardsTarget(Vector3 targetPosition)
    {
        float movementSpeed = smoothing * Time.deltaTime;

        // Calculate the direction from the current position to the target position
        Vector3 direction = targetPosition - selectedObjectRigidbody.position;

        // If the object is far from the target position, move it towards the target
        if (direction.magnitude > distanceThreshold)
        {
            Vector3 newPosition = selectedObjectRigidbody.position + direction.normalized * movementSpeed;
            selectedObjectRigidbody.MovePosition(newPosition);
        }
        else
        {
            // If the object is close to the target position, snap it to the target
            selectedObjectRigidbody.MovePosition(targetPosition);
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        if (!grabbable.isGrabbed)
        {
            PutDownWand();
            return;
        }

        if (!wandPickedUp && (grabbable.grabbedBy.name == "LeftGrabber" || grabbable.grabbedBy.name == "RightGrabber") && grabbable.name == "Wand")
        {
            PickUpWand();
            wandPickedUp = true;  // Set the flag to true once the wand is picked up
        }
    }


    public void PickUpWand()
    {
        holdingWand = true;
        currentDistanceToWand = initialDistanceToWand;
        Debug.Log(holdingWand);
    }

    public void PutDownWand()
    {
        holdingWand = false;
        wandPickedUp = false;
        Debug.Log(holdingWand);
    }
}
