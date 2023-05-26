using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristButtons : MonoBehaviour
{

    //water wand objects
    public GameObject WeffectLight;
    public GameObject WeffectDrip;
    //public GameObject Wwand;
    public GameObject WpositionTop;
    public GameObject WpositionBottom;
    public GameObject Wscript;

    //light wand objects
    public GameObject Leffect;
    public GameObject Lscript;
    //public GameObject Lwand;

    // Start is called before the first frame update
    void Start()
    {
        WaterwandSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void WaterwandSelected()
    {
        WaterwandActive();
        LightWandDeactive();
    }

    public void LightwandSelected()
    {
        LightwandSelected();
        WaterWandDeactivate();
    }

    void WaterwandActive()
    {
        WeffectLight.SetActive(true);
        WeffectDrip.SetActive(true);
        //Wwand.SetActive(true);
        WpositionTop.SetActive(true);
        WpositionBottom.SetActive(true);
        Wscript.SetActive(true);
    }

    void WaterWandDeactivate()
    {
        WeffectLight.SetActive(false);
        WeffectDrip.SetActive(false);
        //Wwand.SetActive(false);
        WpositionTop.SetActive(false);
        WpositionBottom.SetActive(false);
        Wscript.SetActive(false);
    }

    void LightWandActive()
    {
        Leffect.SetActive(true);
        Lscript.SetActive(true);
        //Lwand.SetActive(true);
    }

    void LightWandDeactive()
    {
        Leffect.SetActive(false);
        Lscript.SetActive(false);
        //Lwand.SetActive(false);
    }
}
