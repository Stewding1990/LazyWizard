using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedNPCVisualiser : MonoBehaviour
{
    public GameObject container;

    void Update()
    {
        if (NPCController.selectedNPC != null)
        {
            container.SetActive(true);
            transform.position = NPCController.selectedNPC.transform.position;
        }
        else
        {
            container.SetActive(false);
        }
    }
}
