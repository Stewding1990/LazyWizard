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

    // Levitation Variables
    private Vector3 initialDistanceToWand;
    private Vector3 currentDistanceToWand;
    private Vector3 previousPosition;
    private Rigidbody selectedObjectRigidbody;
    public LayerMask levitateLayer;
    private bool isObjectSelected = false;

    // Smoothness variables
    public float smoothing = 40.0f;
    public float distanceChangeSpeed = 1.0f;
    public float minDistanceToWand = 0.5f;
    public float maxDistanceToWand = 20.0f;
    public float distanceThreshold = 0.2f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        grabbable = GetComponent<CustomGrabbable>();
        initialDistanceToWand = wandTip.transform.localPosition;
        currentDistanceToWand = initialDistanceToWand;
    }

    void FixedUpdate()
    {
        if (holdingWand)
        {
            if (OVRInput.Get(spellButton))
            {
                if (!isObjectSelected)
                {

                    RaycastHit hit;

                    if (Physics.Raycast(wandTip.transform.position, wandTip.transform.forward, out hit, 50f, levitateLayer))
                    {
                        selectedObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                        if (selectedObjectRigidbody != null)
                        {
                            // Calculate the target position for smooth movement
                            Vector3 targetPosition = wandTip.transform.position + wandTip.transform.forward * currentDistanceToWand.z;

                            // Check if the distance to the wand is over the threshold
                            float distanceToWand = Vector3.Distance(selectedObjectRigidbody.transform.position, wandTip.transform.position);

                            // Check if the Y button is pressed
                            if (OVRInput.Get(distanceControlButton))
                            {
                                Debug.Log("Its the right button");
                                if (distanceToWand < maxDistanceToWand)
                                {
                                    // Increase the distance to the wand
                                    currentDistanceToWand.z += distanceChangeSpeed * Time.deltaTime;
                                    currentDistanceToWand.z = Mathf.Clamp(currentDistanceToWand.z, minDistanceToWand, maxDistanceToWand);
                                    Debug.Log(currentDistanceToWand.z);
                                }
                            }
                            else
                            {
                                if (distanceToWand > minDistanceToWand)
                                {
                                    // Decrease the distance to the wand
                                    currentDistanceToWand.z -= distanceChangeSpeed * Time.deltaTime;
                                    currentDistanceToWand.z = Mathf.Clamp(currentDistanceToWand.z, minDistanceToWand, maxDistanceToWand);
                                }
                            }

                            // Disable gravity for the selected object
                            selectedObjectRigidbody.useGravity = false;

                            // Calculate the new target position based on the adjusted distance
                            targetPosition = wandTip.transform.position + wandTip.transform.forward * currentDistanceToWand.z;

                            // Smoothly move the object towards the target position
                            selectedObjectRigidbody.MovePosition(Vector3.Lerp(selectedObjectRigidbody.position, targetPosition, smoothing * Time.deltaTime));
                        }
                    }

                }
                else
                {
                    if (isObjectSelected)
                    {
                        // Re-enable gravity for the selected object
                        if (selectedObjectRigidbody != null)
                        {
                            selectedObjectRigidbody.useGravity = true;
                            selectedObjectRigidbody = null;
                        }
                    }

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!grabbable.isGrabbed)
        {
            PutDownWand();
            return;
        }

        if ((grabbable.grabbedBy.name == "LeftGrabber" || grabbable.grabbedBy.name == "RightGrabber") && grabbable.name == "Wand")
            PickUpWand();
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
        Debug.Log(holdingWand);
    }
}
