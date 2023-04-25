
using UnityEngine;

public class Axe : MonoBehaviour
{
    [HideInInspector] public CustomGrabbable grabbable;
    [HideInInspector] public Rigidbody rigid;

    private float speedToBreak = 1.0f;
    public GameObject axeHitEffect;
    public Transform axeHitPoint;

    void Start()
    {
        grabbable = GetComponent<CustomGrabbable>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Tree tree = other.gameObject.GetComponent<Tree>();

        if (tree == null) return;
        if (!grabbable.isGrabbed) return;
        if (Vector3.Dot(rigid.velocity.normalized, axeHitPoint.transform.forward) < 0.5f) return;
        if (rigid.velocity.magnitude < speedToBreak) return;

        // Vibrate the correct controller.
        if (grabbable.grabbedBy.name == "LeftGrabber")
            SimpleHapticVibrationManager.VibrateController(0.3f, 1.0f, OVRInput.Controller.LTouch);
        else
            SimpleHapticVibrationManager.VibrateController(0.3f, 1.0f, OVRInput.Controller.RTouch);

        // Chop the tree.
        tree.Chop();

        // Instantiate the axe hit effect.
        Instantiate(axeHitEffect, axeHitPoint.position, Quaternion.identity);
    }
}
