using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    [Header("NPC States")]
    public int newState = 0;
    public int currentState = 0;

    [Header("Waypoints")]
    public GameObject[] Waypoints;
    private int waypointIndex = 0;
    public GameObject[] doingOwnThingWayPoints;
    private bool isAnimating = false;
    private int destinationIndex = 0;
    private Vector3 currentDestination;

    private NavMeshAgent agent;

    [Header("Animator")]
    private Animator animator;
    public AnimationClip[] ownThingAnimations;
    public float animationDuration = 2.0f;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.15);
        ChangeState();
        //Lookup state switch
        switch (currentState)
        {
            //Roam
            case 0:
                WalkingAround();
                break;
            case 1:
                //HelpingPlayer();
                break;
            //Attack
            case 2:
                DoingownThing();
                break;
            case 3:
                //StalkPlayerUpdate();
                break;
        }
    }
    private void ChangeState()
    {
        currentState = newState;
    }

    private void WalkingAround()
    {
        // Set the destination of the NavMeshAgent
        agent.SetDestination(Waypoints[waypointIndex].transform.position);

        // Check if the agent has reached the current waypoint
        if (Vector3.Distance(agent.transform.position, Waypoints[waypointIndex].transform.position) <= 1.5f)
        {
            // Update waypoint index
            waypointIndex++;

            // Wrap around to the first waypoint if all waypoints have been visited
            if (waypointIndex >= Waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }

    private void DoingownThing()
    {
        if (isAnimating) return;
        // Set the current destination
        currentDestination = doingOwnThingWayPoints[destinationIndex].transform.position;

        // Move towards the current destination
        agent.SetDestination(currentDestination);

        // Check if the agent has reached the current destination
        if (Vector3.Distance(agent.transform.position, currentDestination) < 2)
        {
            agent.isStopped = true;
            StartCoroutine(PlayAnimationAndWait());
        }
    }

    private IEnumerator PlayAnimationAndWait()
    {
        // Set the flag to indicate an animation is playing
        isAnimating = true;

        // Perform the animation based on the destination index
        switch (destinationIndex)
        {
            case 0:
                animator.SetBool("isDrinking", true);
                break;
            case 1:
                animator.SetBool("isInteracting", true);
                break;
            case 2:
                animator.SetBool("isBuilding", true);
                break;
            case 3:
                animator.SetBool("isBuilding", true);
                break;
            // Add more cases for additional animations

            default:
                break;
        }

        // Wait for the animation to finish playing
        yield return new WaitForSeconds(animationDuration);

        // Reset the animation bool
        switch (destinationIndex)
        {
            case 0:
                animator.SetBool("isDrinking", false);
                break;
            case 1:
                animator.SetBool("isInteracting", false);
                break;
            case 2:
                animator.SetBool("isBuilding", false);
                break;
            case 3:
                animator.SetBool("isBuilding", false);
                break;
            // Add more cases for additional animations

            default:
                break;
        }

        // Move to the next destination
        destinationIndex++;
        if (destinationIndex >= doingOwnThingWayPoints.Length)
        {
            destinationIndex = 0;
        }

        // Resume movement towards the next destination
        agent.isStopped = false;

        // Reset the flag after animation is finished
        isAnimating = false;
    }

}
