using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour : MonoBehaviour
{

    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool pouring = false;
    private Stream currentStream = null;


    private void Update()
    {
        bool pourCheck = calculatePouringAngle() < pourThreshold;

        if(pouring != pourCheck)
        {
            pouring = pourCheck;

            if (pouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        print("start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("end");
    }

    private float calculatePouringAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}
