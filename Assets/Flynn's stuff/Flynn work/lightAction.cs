using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAction : MonoBehaviour
{


    public GameObject lightTrigger;
    public ParticleSystem lightEffect;
    public GameObject lighteffectGO;

    // Start is called before the first frame update
    void Start()
    {
        lightEffect.Stop();

        lightTrigger.SetActive(false);
        lighteffectGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            lighteffectGO.SetActive(true);
            lightEffect.Play();
            lightTrigger.SetActive(true);

        }
        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            lighteffectGO.SetActive(false);
            lightEffect.Stop();
            lightTrigger.SetActive(false);
        }


       
        
    }
}
