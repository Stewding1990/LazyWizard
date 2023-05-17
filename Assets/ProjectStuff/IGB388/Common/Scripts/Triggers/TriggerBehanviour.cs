using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehanviour : MonoBehaviour
{
    public enum TriggerState
    {
        HelpingPlayer,
        DoingOwnThing,
        WalkingAround,
        Something
    };

    [Header("Trigger Variables")]
    public TriggerState enter;
    public TriggerState exit;

    [Header("Player")]
    public GameObject Player;

    [Header("Waypoints")]
    public GameObject[] Waypoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (enter)
            {
                case TriggerState.HelpingPlayer:
                    ChangeNpcBehaviour(0);
                    break;
                case TriggerState.DoingOwnThing:
                    ChangeNpcBehaviour(1);
                    break;
                case TriggerState.WalkingAround:
                    ChangeNpcBehaviour(2);
                    break;
                case TriggerState.Something:
                    ChangeNpcBehaviour(3);
                    break;
            }
        }
    }

    private void ChangeNpcBehaviour(int state)
    {
        Player.GetComponent<NPCBehaviour>().newState = state;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (enter)
            {
                case TriggerState.HelpingPlayer:
                    ChangeNpcBehaviour(0);
                    break;
                case TriggerState.DoingOwnThing:
                    ChangeNpcBehaviour(1);
                    break;
                case TriggerState.WalkingAround:
                    ChangeNpcBehaviour(2);
                    break;
                case TriggerState.Something:
                    ChangeNpcBehaviour(3);
                    break;
            }
        }
    }
}
