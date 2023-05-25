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
    private Coroutine firePlaceCoroutine;

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
                    break;
                case "WeaponChestTrigger":
                    Debug.Log("Weapon Chest");
                    break;
                case "FireWoodTrigger":
                    Debug.Log("Firewood");
                    StartFirePlaceActivity();
                    break;
                case "PlantTrigger":
                    Debug.Log("Plant");
                    break;
                case "DishesTrigger":
                    Debug.Log("Dishes");
                    break;
                case "WeaponRackTrigger":
                    Debug.Log("WeaponRack");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopFirePlaceActivity();
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

    private void StartFirePlaceActivity()
    {
        if (firePlaceCoroutine == null)
        {
            firePlaceCoroutine = StartCoroutine(FirePlaceActivityCoroutine());
        }
    }

    private void StopFirePlaceActivity()
    {
        if (firePlaceCoroutine != null)
        {
            StopCoroutine(firePlaceCoroutine);
            firePlaceCoroutine = null;
        }
    }

    private IEnumerator FirePlaceActivityCoroutine()
    {
        while (true)
        {
            npcBehaviour.FirePlaceActivity();

            // Wait for the next frame
            yield return null;
        }
    }
}
