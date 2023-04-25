using UnityEngine;

public class Pot : MonoBehaviour
{
    [HideInInspector] public CustomGrabbable grabbable;
    [HideInInspector] public Rigidbody rigid;

    private float speedToBreak = 1.0f;
    public GameObject potSmashEffect;

    void Start()
    {
        grabbable = GetComponent<CustomGrabbable>();
        
        
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;    
    
    }

    private void OnTriggerEnter(Collider other)
    {
        Pot collidingPot = other.gameObject.GetComponent<Pot>();

        // Check all the guard conditions.
        if (collidingPot == null) return;
        if (!grabbable.isGrabbed) return;
        if (!collidingPot.grabbable.isGrabbed) return;
        if (rigid.velocity.magnitude + collidingPot.rigid.velocity.magnitude < speedToBreak) return;


        SimpleHapticVibrationManager.VibrateController(0.3f, 1.0f, OVRInput.Controller.LTouch);
        SimpleHapticVibrationManager.VibrateController(0.3f, 1.0f, OVRInput.Controller.RTouch);
        Instantiate(potSmashEffect, transform.position, Quaternion.identity);
        Instantiate(potSmashEffect, collidingPot.transform.position, Quaternion.identity);



        // Smash the pots.
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}

