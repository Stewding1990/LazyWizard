using System.Collections;
using UnityEngine;

public class Levitationspell : MonoBehaviour
{
    [HideInInspector] public CustomGrabbable grabbable;
    [HideInInspector] public Rigidbody rigid;
    public OVRInput.Button spellButton = OVRInput.Button.PrimaryIndexTrigger;
    public GameObject wandTip;
    public LineRenderer lineRenderer;

    private bool holdingWand = false;
    private Rigidbody selectedObjectRigidbody;
    private Coroutine levitationCoroutine;

    // Levitation Variables
    public float floatHeight = 1f;
    public float floatSpeed = 0.5f;
    public float moveSpeed = 0.1f;
    public float moveSpeedZ = 0.05f;
    private float distanceToWand;

    private RaycastHit hit;

    private void Start()
    {
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void Update()
    {
        if (holdingWand && OVRInput.Get(spellButton))
        {
            if (Physics.Raycast(wandTip.transform.position, wandTip.transform.forward, out hit))
            {
                selectedObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (selectedObjectRigidbody != null)
                {
                    StartLevitation();
                }
            }

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, wandTip.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            StopLevitation();

            lineRenderer.enabled = false;
        }
    }

    private void StartLevitation()
    {
        if (levitationCoroutine == null)
        {
            // Disable gravity for the selected object
            selectedObjectRigidbody.useGravity = false;

            // Start the levitation coroutine
            levitationCoroutine = StartCoroutine(LevitateObject());
        }
    }

    private void StopLevitation()
    {
        if (levitationCoroutine != null)
        {
            // Re-enable gravity for the selected object
            selectedObjectRigidbody.useGravity = true;

            // Stop the levitation coroutine
            StopCoroutine(levitationCoroutine);
            levitationCoroutine = null;
        }
    }

    private IEnumerator LevitateObject()
    {
        while (true)
        {
            distanceToWand = 2.0f; // Set the distance between wand tip and object
            Vector3 targetPosition = wandTip.transform.position + wandTip.transform.forward * ((distanceToWand + hit.distance) * 1.2f);

            // Smoothly move the object towards the target position
            while (Vector3.Distance(selectedObjectRigidbody.transform.position, targetPosition) > 0.01f)
            {
                selectedObjectRigidbody.MovePosition(Vector3.Lerp(selectedObjectRigidbody.transform.position, targetPosition, moveSpeed * Time.deltaTime));
                yield return null;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!holdingWand)
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
        Debug.Log("Wand picked up");
    }

    public void PutDownWand()
    {
        holdingWand = false;
        Debug.Log("Wand put down");
    }
}
