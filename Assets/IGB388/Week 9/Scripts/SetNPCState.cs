using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNPCState : MonoBehaviour
{
    public NPCController.Activity activity;
    public float validStoppingSitance = 2.2f;

    private void OnMouseDown()
    {
        if (NPCController.selectedNPC != null)
        {
            NPCController.selectedNPC.SetNextActivity(activity, transform.position, validStoppingSitance);
        }
    }
}
