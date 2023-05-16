using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAction : MonoBehaviour
{


    public GameObject lightTrigger;
    public ParticleSystem lightEffect;

    // Start is called before the first frame update
    void Start()
    {
        lightEffect.Stop();

        lightTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            
            lightEffect.Play();
            lightTrigger.SetActive(true);

        }
        else
        {
            lightEffect.Stop();
            lightTrigger.SetActive(false);
        }
        
    }
}
