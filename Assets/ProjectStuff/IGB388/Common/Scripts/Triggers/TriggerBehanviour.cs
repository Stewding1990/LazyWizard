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
            Debug.Log(npcBehaviour.currentState);

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
                    StartActivity(AudioManager.Instance.bookshelfIncompletedialogueClips, AudioManager.Instance.bookshelfCompletedialogueClips,
                        npcBehaviour.bookshelfIncompleteCandles, npcBehaviour.bookshelfCompleteCandles);
                    break;
                case "WeaponChestTrigger":
                    Debug.Log("Weapon Chest");
                    StartActivity(AudioManager.Instance.weaponChestIncompletedialogueClips, AudioManager.Instance.weaponChestCompletedialogueClips,
                        npcBehaviour.trashBinIncompleteCandles, npcBehaviour.trashBinCompleteCandles);
                    break;
                case "FireWoodTrigger":
                    Debug.Log("Firewood");
                    StartActivity(AudioManager.Instance.fireWoodIncompletedialogueClips, AudioManager.Instance.fireWoodCompletedialogueClips,
                        npcBehaviour.firePlaceIncompleteCandles, npcBehaviour.firePlaceCompleteCandles);
                    break;
                case "PlantTrigger":
                    Debug.Log("Plant");
                    StartActivity(AudioManager.Instance.plantIncompletedialogueClips, AudioManager.Instance.plantCompletedialogueClips,
                        npcBehaviour.plantIncompleteCandles, npcBehaviour.plantCompleteCandles);
                    break;
                case "DishesTrigger":
                    Debug.Log("Dishes");
                    StartActivity(AudioManager.Instance.dishesIncompletedialogueClips, AudioManager.Instance.dishesCompletedialogueClips,
                        npcBehaviour.dishesIncompleteCandles, npcBehaviour.dishesCompleteCandles);
                    break;
                case "WeaponRackTrigger":
                    Debug.Log("WeaponRack");
                    StartActivity(AudioManager.Instance.weaponChestIncompletedialogueClips, AudioManager.Instance.weaponChestCompletedialogueClips,
                        npcBehaviour.weaponStandIncompleteCandles, npcBehaviour.weaponStandCompleteCandles);
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
            Debug.Log(npcBehaviour.currentState);
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

    private void StartActivity(AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete, GameObject IncompleteCandles, GameObject CompleteCandles)
    {
        if (activityCoroutine == null)
        {
            activityCoroutine = StartCoroutine(ActivityCoroutine(activityDialogueIncomplete, activityDialogueComplete, IncompleteCandles, CompleteCandles));
        }
    }



    private IEnumerator ActivityCoroutine(AudioClip[] activityDialogueIncomplete, AudioClip[] activityDialogueComplete, GameObject IncompleteCandles, GameObject CompleteCandles)
    {
        while (true)
        {
            npcBehaviour.NPCHelpingActivity(activityDialogueIncomplete, activityDialogueComplete, IncompleteCandles, CompleteCandles);

            // Wait for the next frame
            yield return null;
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
}
