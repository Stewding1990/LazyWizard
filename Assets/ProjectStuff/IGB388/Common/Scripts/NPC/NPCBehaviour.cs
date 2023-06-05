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

    public bool dialoguePlayed = false; // Flag to track if dialogue has been played
                                        // Declare a variable to track the time
    private float dialogueTimer = 0f;
    private float dialogueInterval = 12f; // Interval in seconds


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
        if (agent.velocity.magnitude > 0.15)
        {
            animator.SetBool("isWalking", true);
            AudioManager.Instance.PlayLoopingSoundEffect(AudioManager.Instance.walkingSFX);
        }
        else
        {
            animator.SetBool("isWalking", false);
            AudioManager.Instance.StopLoopingSoundEffect();
        }

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
        agent.stoppingDistance = 0.5f;
        currentDestination = Waypoints[waypointIndex].transform.position;

        float distanceToDestination = Vector3.Distance(agent.transform.position, currentDestination);

        // Check if the agent has reached the current waypoint
        if (distanceToDestination > agent.stoppingDistance)
        {
            agent.SetDestination(currentDestination);
        }
        else
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

        agent.stoppingDistance = 0.5f;

        if (destinationIndex >= doingOwnThingWayPoints.Length)
        {
            destinationIndex = 0;
        }

        // Set the current destination
        currentDestination = doingOwnThingWayPoints[destinationIndex].transform.position;

        // Check the distance to the current destination
        float distanceToDestination = Vector3.Distance(agent.transform.position, currentDestination);

        if (distanceToDestination > agent.stoppingDistance)
        {
            // Move towards the current destination
            agent.SetDestination(currentDestination);
            agent.isStopped = false;
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Stop the agent and play animation
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
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
                AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buildingSFX);
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

        // Resume movement towards the next destination
        agent.isStopped = false;

        // Reset the flag after animation is finished
        isAnimating = false;

        // Move to the next destination
        destinationIndex++;
    }



    private bool isDialoguePlaying = false; // Flag to track if dialogue is currently playing

    public void NPCHelpingActivity(AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete, GameObject IncompleteCandles, GameObject CompleteCandles)
    {
        agent.stoppingDistance = 3f;
        float distanceToPlayer = Vector3.Distance(agent.transform.position, Player.transform.position);
        Debug.Log(distanceToPlayer);

        if (distanceToPlayer > agent.stoppingDistance)
        {
            MoveToDestination();
        }
        else
        {
            StopMoving();

            if (!isDialoguePlaying)
            {
                StartCoroutine(PlayDialogueOnce(activityDialogueIncomplete, activityDialogueComplete, IncompleteCandles, CompleteCandles));
            }
        }
    }

    private IEnumerator PlayDialogueOnce(AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete, GameObject IncompleteCandles, GameObject CompleteCandles)
    {
        isDialoguePlaying = true;

        // Play random dialogue based on the state of candles
        if (IncompleteCandles.activeSelf)
        {
            AudioManager.Instance.PlayRandomDialogueClip(activityDialogueIncomplete);
        }
        else if (CompleteCandles.activeSelf)
        {
            AudioManager.Instance.PlayRandomDialogueClip(activityDialogueComplete);
        }

        ActivateAnimation();

        // Wait for the dialogue to finish playing
        yield return new WaitForSeconds(dialogueInterval);

        ResetDialoguePlayedFlag();
        isDialoguePlaying = false;
    }

    private void MoveToDestination()
    {
        agent.SetDestination(Player.transform.position);
        agent.isStopped = false;
        animator.SetBool("isWalking", true);
    }

    private void StopMoving()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
    }

    private void ResetDialoguePlayedFlag()
    {
        dialoguePlayed = false; // Reset the flag to false
        dialogueTimer = 0f; // Reset the timer
    }
}
