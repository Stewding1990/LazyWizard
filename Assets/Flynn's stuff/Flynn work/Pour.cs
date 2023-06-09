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
    public ParticleSystem Wlight;


    public GameObject waterTrigger;

    public void Start()
    {
        waterTrigger.SetActive(false);
    }

    private void Update()
    {
        bool pourCheck = calculatePouringAngle() < pourThreshold;


        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            spark.Play();
            Wlight.Play();

            if (pouring != pourCheck)
            {
                pouring = pourCheck;

                if (pouring)
                {
                    StartPour();
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.waterSpellSFX);

                    waterTrigger.SetActive(true);
                }
                else
                {
                    EndPour();

                    waterTrigger.SetActive(false);
                }
            }
        }
        else
        {
            spark.Stop();
            Wlight.Stop();
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
