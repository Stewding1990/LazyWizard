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

    [Header("Player Reference")]
    public GameObject Player;

    private NavMeshAgent agent;

    [Header("Animator")]
    private Animator animator;
    public AnimationClip[] ownThingAnimations;
    private float[] animationDurations; // Array to store animation clip durations
    public string isTalking = "isTalking";

    [Header("Candles for Each activity")]
    public GameObject bookshelfIncompleteCandles;
    public GameObject bookshelfCompleteCandles;
    public GameObject firePlaceIncompleteCandles;
    public GameObject firePlaceCompleteCandles;
    public GameObject trashBinIncompleteCandles;
    public GameObject trashBinCompleteCandles;
    public GameObject weaponStandCompleteCandles;
    public GameObject weaponStandIncompleteCandles;
    public GameObject dishesCompleteCandles;
    public GameObject dishesIncompleteCandles;
    public GameObject plantCompleteCandles;
    public GameObject plantIncompleteCandles;

    private bool dialoguePlayed = false; // Flag to track if dialogue has been played


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Initialize animation duration array
        animationDurations = new float[ownThingAnimations.Length];

        // Calculate and store animation clip durations
        for (int i = 0; i < ownThingAnimations.Length; i++)
        {
            animationDurations[i] = ownThingAnimations[i].length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.15);
        ChangeState();

        // Lookup state switch
        switch (currentState)
        {
            // Roam
            case 0:
                WalkingAround();
                break;
            case 1:
                // HelpingPlayer();
                break;
            // Attack
            case 2:
                DoingownThing();
                break;
            case 3:
                // StalkPlayerUpdate();
                break;
        }
    }

    private void ChangeState()
    {
        currentState = newState;
    }

    public void ActivateAnimation()
    {
        animator.SetTrigger(isTalking);
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

        // Set the "isWalking" parameter to false to stop the walking animation
        animator.SetBool("isWalking", false);

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

        // Get the duration of the current animation clip
        float clipDuration = animationDurations[destinationIndex];

        // Wait for the animation to finish playing
        yield return new WaitForSeconds(clipDuration * 1.2f);

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

        // Set the "isWalking" parameter to true to resume the walking animation
        animator.SetBool("isWalking", true);

        // Reset the flag after animation is finished
        isAnimating = false;
    }

    // Declare a variable to track the time
    private float dialogueTimer = 0f;
    private float dialogueInterval = 5f; // Interval in seconds

    public void NPCHelpingActivity(int waypointIndex, AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete, GameObject IncompleteCandles, GameObject CompleteCandles)
    {
        agent.SetDestination(Waypoints[waypointIndex].transform.position);

        if (Vector3.Distance(agent.transform.position, Waypoints[waypointIndex].transform.position) < 2f)
        {
            Debug.Log("here");
            agent.isStopped = true;
            agent.transform.LookAt(Player.transform);

            if (!dialoguePlayed) // Check if dialogue has already been played
            {
                dialogueTimer += Time.deltaTime; // Increment the timer
                Debug.Log(dialogueTimer);

                // Check if the timer has reached the interval
                if (dialogueTimer >= dialogueInterval)
                {
                    // Play random dialogue based on the state of candles
                    if (bookshelfIncompleteCandles.activeSelf)
                    {
                        Debug.Log("I want to play sound");
                        AudioManager.Instance.PlayRandomDialogueClip(activityDialogueIncomplete);
                        ActivateAnimation();
                        ResetDialoguePlayedFlag(); // Reset the flag
                    }
                    else if (bookshelfCompleteCandles.activeSelf)
                    {
                        Debug.Log("I want to play sound x 2");
                        AudioManager.Instance.PlayRandomDialogueClip(activityDialogueComplete);
                        ActivateAnimation();
                        ResetDialoguePlayedFlag(); // Reset the flag
                    }
                }
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }

    private void ResetDialoguePlayedFlag()
    {
        Debug.Log("Too early");
        dialoguePlayed = false; // Reset the flag to false
        dialogueTimer = 0f; // Reset the timer
    }





}
