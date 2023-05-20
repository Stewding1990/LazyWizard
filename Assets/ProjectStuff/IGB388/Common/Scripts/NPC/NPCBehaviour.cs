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

    private NavMeshAgent agent;

    [Header("Animator")]
    private Animator animator;

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
        if(currentState != 0)
        {
            Debug.Log("we changed");
        }
        ChangeState();
        //Lookup state switch
        switch (currentState)
        {
            //Roam
            case 0:
                Roam();
                break;
            case 1:
                Debug.Log("changed here");
                //Hide();
                break;
            //Attack
            case 2:
                //Attack();
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

    private void Roam()
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

}
