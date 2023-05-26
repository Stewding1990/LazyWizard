using System.Collections;
using UnityEngine;

public class TriggerBehanviour : MonoBehaviour
{
    public enum TriggerState
    {
        WalkingAround,
        HelpingPlayer,
        DoingOwnThing,
        Something
    };

    [Header("Trigger Variables")]
    public TriggerState enter;
    public TriggerState exit;

    [Header("Player")]
    public NPCBehaviour npcBehaviour;
    public GameObject NPC;
    private bool isDoingTask = false;
    private Coroutine activityCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcBehaviour = NPC.GetComponent<NPCBehaviour>();

            // Update the NPC's state
            ChangeNpcBehaviour(enter);

            // Wait for the next frame to ensure the state has been updated
            StartCoroutine(ProcessTriggerActions());
        }
    }

    private IEnumerator ProcessTriggerActions()
    {
        // Wait for the next frame to ensure the state has been updated
        yield return null;

        // Check if the current state is 1 (assuming 1 represents the state you're interested in)
        if (npcBehaviour.currentState == 1)
        {
            // Perform different actions based on the specific trigger
            switch (gameObject.name)
            {
                case "BookshelfTrigger":
                    Debug.Log("Bookshelf");
                    StartActivity(0, AudioManager.Instance.bookshelfIncompletedialogueClips, AudioManager.Instance.bookshelfCompletedialogueClips);
                    break;
                case "WeaponChestTrigger":
                    Debug.Log("Weapon Chest");
                    StartActivity(5, AudioManager.Instance.weaponChestIncompletedialogueClips, AudioManager.Instance.weaponChestCompletedialogueClips);
                    break;
                case "FireWoodTrigger":
                    Debug.Log("Firewood");
                    StartActivity(4, AudioManager.Instance.fireWoodIncompletedialogueClips, AudioManager.Instance.fireWoodCompletedialogueClips);
                    break;
                case "PlantTrigger":
                    Debug.Log("Plant");
                    StartActivity(2, AudioManager.Instance.plantIncompletedialogueClips, AudioManager.Instance.plantCompletedialogueClips);
                    break;
                case "DishesTrigger":
                    Debug.Log("Dishes");
                    StartActivity(2, AudioManager.Instance.dishesIncompletedialogueClips, AudioManager.Instance.dishesCompletedialogueClips);
                    break;
                case "WeaponRackTrigger":
                    Debug.Log("WeaponRack");
                    StartActivity(3, AudioManager.Instance.weaponChestIncompletedialogueClips, AudioManager.Instance.weaponChestCompletedialogueClips);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopActivity();
            ChangeNpcBehaviour(exit);
        }
    }

    private void ChangeNpcBehaviour(TriggerState state)
    {
        if (npcBehaviour != null)
        {
            int stateIndex = (int)state;
            npcBehaviour.newState = stateIndex;
        }
        else
        {
            Debug.Log("NPCBehaviour component not found.");
        }
    }

    private void StartActivity(int waypointIndex, AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete)
    {
        if (activityCoroutine == null)
        {
            activityCoroutine = StartCoroutine(ActivityCoroutine(waypointIndex, activityDialogueIncomplete, activityDialogueComplete));
        }
    }

    private void StopActivity()
    {
        if (activityCoroutine != null)
        {
            StopCoroutine(activityCoroutine);
            activityCoroutine = null;
        }
    }

    private IEnumerator ActivityCoroutine(int waypointIndex, AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete)
    {
        while (true)
        {
            npcBehaviour.NPCHelpingActivity(waypointIndex, activityDialogueIncomplete, activityDialogueComplete);

            // Wait for the next frame
            yield return null;
        }
    }
}
