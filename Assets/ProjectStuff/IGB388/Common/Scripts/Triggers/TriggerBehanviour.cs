using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
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
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enter == TriggerState.HelpingPlayer)
            {
                ChangeNpcBehaviour(enter);

                // Perform different actions based on the specific trigger
                switch (gameObject.name)
                {
                    case "BookshelfTrigger":
                        // Action for Trigger1
                        break;
                    case "WeaponChestTrigger":
                        // Action for Trigger2
                        break;
                    // Add more cases for other triggers as needed
                    case "FireWoodTrigger":
                        // Action for Trigger1
                        break;
                    case "PlantTrigger":
                        // Action for Trigger2
                        break;
                    case "DishesTrigger":
                        // Action for Trigger1
                        break;
                    case "WeaponRackTrigger":
                        // Action for Trigger2
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeNpcBehaviour(exit);
        }
    }

    private void ChangeNpcBehaviour(TriggerState state)
    {
        int stateIndex = (int)state;
        Player.GetComponent<NPCBehaviour>().newState = stateIndex;
    }
}
