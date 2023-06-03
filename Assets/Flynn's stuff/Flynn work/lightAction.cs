using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAction : MonoBehaviour
{


    public GameObject lightTrigger;
    public ParticleSystem lightEffect;
    public GameObject lighteffectGO;

    public float VibDuration;
    public float VibStrength;

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

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            lighteffectGO.SetActive(true);
            lightEffect.Play();
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.sunSpellSFX);
            lightTrigger.SetActive(true);

            SimpleHapticVibrationManager.VibrateController(VibDuration, VibStrength, OVRInput.Controller.RTouch);

        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            lighteffectGO.SetActive(false);
            lightEffect.Stop();
            lightTrigger.SetActive(false);

            SimpleHapticVibrationManager.VibrateController(0f, 0f, OVRInput.Controller.RTouch);
        }


       
        
    }
}
