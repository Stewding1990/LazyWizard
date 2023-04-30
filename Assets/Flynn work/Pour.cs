using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour : MonoBehaviour
{

    public int pourThreshold = 45;
    public Transform origin = null;
    //public GameObject streamPrefab = null;

    private bool pouring = false;
    private Stream currentStream = null;

    public ParticleSystem drip;
    public ParticleSystem spark;
    public ParticleSystem light;

    private void Update()
    {
        bool pourCheck = calculatePouringAngle() < pourThreshold;


        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            spark.Play();
            light.Play();

            if (pouring != pourCheck)
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
        else
        {
            spark.Stop();
            light.Stop();
        }


        
    }

    private void StartPour()
    {
        print("start");

        drip.Play();

        //currentStream = CreateStream();
        //currentStream.Begin();
    }

    private void EndPour()
    {
        print("end");

        drip.Stop();
    }

    private float calculatePouringAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

   
}
