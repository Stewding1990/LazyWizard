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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            npcBehaviour = NPC.GetComponent<NPCBehaviour>();
            ChangeNpcBehaviour(enter);
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            ChangeNpcBehaviour(exit);
        }
    }

    private void ChangeNpcBehaviour(TriggerState state)
    {
        if (npcBehaviour != null)
        {
            int stateIndex = (int)state;
            npcBehaviour.newState = stateIndex;
            Debug.Log(npcBehaviour.newState);
        }
        else
        {
            Debug.Log("NPCBehaviour component not found.");
        }
    }

}
