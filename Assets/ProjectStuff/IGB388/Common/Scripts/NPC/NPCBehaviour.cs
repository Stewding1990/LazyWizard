using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [Header("NPC States")]
    public int newState = 0;
    protected int currentState = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        //Lookup state switch
        switch (currentState)
        {
            //Roam
            case 0:
                //Roam();
                break;
            //Hide
            case 1:
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
}
