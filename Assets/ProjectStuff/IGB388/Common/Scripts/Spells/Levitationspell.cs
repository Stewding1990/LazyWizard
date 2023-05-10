using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitationspell : MonoBehaviour
{
    [HideInInspector] public CustomGrabbable grabbable;
    [HideInInspector] public Rigidbody rigid;
    public OVRInput.Button spellButton = OVRInput.Button.PrimaryIndexTrigger;
    public float levitationForce = 20f;
    public GameObject wandTip;
    private bool holdingWand = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        grabbable = GetComponent<CustomGrabbable>();
    }
    private void Update()
    {
        // Check if player is holding the wand
        if (holdingWand)
        {
            // Check if the spell key is pressed
            if (OVRInput.GetDown(spellButton))
            {

                // Cast the spell
                RaycastHit hit;
                if (Physics.Raycast(wandTip.transform.position, wandTip.transform.forward, out hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawLine(wandTip.transform.position, hit.point, Color.green, 10f);
                    // Apply levitation force to the object hit by the raycast
                    Rigidbody selectedObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                    if (selectedObjectRigidbody != null)
                    {
                        Debug.Log(selectedObjectRigidbody);
                        selectedObjectRigidbody.AddForce(transform.up * levitationForce, ForceMode.Impulse);
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
        Debug.Log(holdingWand);
    }

    // Function to set the holdingWand variable to false when the wand is put down
    public void PutDownWand()
    {
        holdingWand = false;
        Debug.Log(holdingWand);
    }
}