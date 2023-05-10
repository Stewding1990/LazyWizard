using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitationspell : MonoBehaviour
{
    [HideInInspector] public CustomGrabbable grabbable;
    [HideInInspector] public Rigidbody rigid;
    public OVRInput.Button spellButton = OVRInput.Button.PrimaryIndexTrigger;
    public GameObject wandTip;
    private bool holdingWand = false;

    //Levitation Variables
    public float floatHeight = 1f;
    public float floatSpeed = 0.5f;
    public float moveSpeed = 0.1f;
    public float moveSpeedZ = 0.05f;
    private float distanceToWand;
    private Vector3 previousPosition;
    private Rigidbody selectedObjectRigidbody;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        grabbable = GetComponent<CustomGrabbable>();
    }
void FixedUpdate()
{
    if (holdingWand)
    {
        if (OVRInput.Get(spellButton))
        {
            RaycastHit hit;

            if (Physics.Raycast(wandTip.transform.position, wandTip.transform.forward, out hit))
            {
                selectedObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (selectedObjectRigidbody != null)
                {
                    //distanceToWand = Vector3.Distance(selectedObjectRigidbody.transform.position, wandTip.transform.position);
                    Debug.Log(selectedObjectRigidbody.gameObject.name);

                    // disable gravity for the selected object
                    selectedObjectRigidbody.useGravity = false;

                    // apply the force to the object's rigidbody
                    float smoothing = 40.0f;
                    float distanceToWand = 2.0f; // Set the distance between wand tip and object
                    Vector3 targetPosition = wandTip.transform.position + wandTip.transform.forward * ((distanceToWand + hit.distance) * 2);
                    selectedObjectRigidbody.MovePosition(Vector3.Lerp(wandTip.transform.position, targetPosition, smoothing * Time.deltaTime));
                }
            }
        }
            else
            {
                // re-enable gravity for the selected object
                if (selectedObjectRigidbody != null)
                {
                    selectedObjectRigidbody.useGravity = true;
                    selectedObjectRigidbody = null;
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
        Debug.Log(holdingWand);
    }

    // Function to set the holdingWand variable to false when the wand is put down
    public void PutDownWand()
    {
        holdingWand = false;
        Debug.Log(holdingWand);
    }
}