using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    // The current/next activity the player will perform.
    public enum Activity { None, Mine, Fish, Harvest, Build }
    private Activity nextActivity;

    public GameObject miningObjects, fishingObjects, buildingObjects, harvestingObjects;
    private bool activityInProgress, isHarvesting, isBuilding, isMining, isFishing, isWalking;

    // Cached references.
    private Animator animator;
    private NavMeshAgent agent;

    // Keep track on the NPC the player has "selected".
    public static NPCController selectedNPC = null;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        selectedNPC = this;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", agent.velocity.magnitude > 0.15);
        ClickToMove();


        if (!activityInProgress && nextActivity != Activity.None && agent.pathStatus == NavMeshPathStatus.PathComplete &&
            Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
        {
            BeginNextActivity();
        }
    }

    /// <summary>
    /// Set the next activity the player will complete. They must reach the specified location
    /// before the activity can begin.
    /// </summary>
    public void SetNextActivity (Activity activity, Vector3 location, float stoppingDistance = 0)
    {
        if (activityInProgress) EndActivity();

        nextActivity = activity;
        agent.stoppingDistance = stoppingDistance;
        agent.SetDestination(location);
    }

    /// <summary>
    /// Begins the next activity, displaying the correct tools and setting the correct animator params.
    /// </summary>
    private void BeginNextActivity ()
    {
        activityInProgress = true;
        if (miningObjects != null) miningObjects.SetActive(isMining = nextActivity == Activity.Mine);
        if (fishingObjects != null) fishingObjects.SetActive(isFishing = nextActivity == Activity.Fish);
        if (buildingObjects != null) buildingObjects.SetActive(isBuilding = nextActivity == Activity.Build);
        if (harvestingObjects != null) harvestingObjects.SetActive(isHarvesting = nextActivity == Activity.Harvest);
        UpdateAnimatorParameters();
    }

    /// <summary>
    /// End the current activity, disabling the tools and setting the correct animator params.
    /// </summary>
    private void EndActivity ()
    {
        activityInProgress = false;
        nextActivity = Activity.None;
        if (miningObjects != null) miningObjects.SetActive(isMining = false);
        if (fishingObjects != null) fishingObjects.SetActive(isFishing = false);
        if (buildingObjects != null) buildingObjects.SetActive(isBuilding = false);
        if (harvestingObjects != null) harvestingObjects.SetActive(isHarvesting = false);
        UpdateAnimatorParameters();
    }

    /// <summary>
    /// Update all of the properties for the unique actions.
    /// </summary>
    private void UpdateAnimatorParameters()
    {
        
        animator.SetBool("Harvesting", isHarvesting);
        animator.SetBool("Building", isBuilding);
        animator.SetBool("Mining", isMining);
        animator.SetBool("Fishing", isFishing);
    }

    /// <summary>
    /// Set a destination through the NavMesh when the player clicks on the ground.
    /// </summary>
    private void ClickToMove ()
    {
        if (selectedNPC == this && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.name == "Ground")
                {
                    SetNextActivity(Activity.None, hit.point);
                }
            }
        }
    }

    /// <summary>
    /// Set this NPC as the "selected" one if a player clicks on it.
    /// </summary>
    private void OnMouseDown()
    {
        selectedNPC = this;
    }
}
