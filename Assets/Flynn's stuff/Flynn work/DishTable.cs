using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishTable : MonoBehaviour
{
    public GameObject IncompleteCandles;
    public GameObject CompleteCandles;

    public GameObject spoon1;
    public GameObject plate1;
    public GameObject plate2;
    public GameObject cuttingboard;
    public GameObject spoon2;
    public GameObject bowl1;
    public GameObject bowl2;

    public GameObject spoon1Dirt;
    public GameObject plate1Dirt;
    public GameObject plate2Dirt;
    public GameObject cuttingboardDirt;
    public GameObject spoon2Dirt;
    public GameObject bowl1Dirt;
    public GameObject bowl2Dirt;

    public GameObject spoon1Wet;
    public GameObject plate1Wet;
    public GameObject plate2Wet;
    public GameObject cuttingboardWet;
    public GameObject spoon2Wet;
    public GameObject bowl1Wet;
    public GameObject bowl2Wet;

    public static bool spoon1Clean = false;
    public static bool plate1Clean = false;
    public static bool plate2Clean = false;
    public static bool cuttingboardClean = false;
    public static bool spoon2Clean = false;
    public static bool bowl1Clean = false;
    public static bool bowl2Clean = false;

    bool spoon1Done = false;
    bool plate1Done = false;
    bool plate2Done = false;
    bool cuttingboardDone = false;
    bool spoon2Done = false;
    bool bowl1Done = false;
    bool bowl2Done = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spoon1Clean == true)
        {
            spoon1Dirt.SetActive(false);
            spoon1Wet.SetActive(true);
            spoon1.SetActive(false);
        }

        if (plate1Clean == true)
        {
            plate1Wet.SetActive(true);
        }

        if (plate2Clean == true)
        {
            plate2Wet.SetActive(true);
        }

        if (cuttingboardClean == true)
        {
            cuttingboardWet.SetActive(true);
        }

        if (spoon2Clean == true)
        {
            spoon2Wet.SetActive(true);
        }

        if (bowl1Clean == true)
        {
            bowl1Wet.SetActive(true);
        }

        if (bowl2Clean == true)
        {
            bowl2Wet.SetActive(true);
        }


        if (spoon1Done == true & plate1Done == true & plate2Done == true & cuttingboardDone == true & spoon2Done == true & bowl1Done == true & bowl2Done == true)
        {
            IncompleteCandles.SetActive(false);
            CompleteCandles.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (spoon1Clean == true)
        {
            if (other.tag == "Light")
            {
                spoon1Wet.SetActive(false);
                spoon1.SetActive(true);
                spoon1Done = true;
            }

        }

        if (plate1Clean == true)
        {
            if (other.tag == "Light")
            {
                plate1Wet.SetActive(false);
                plate1Done = true;
            }

        }

        if (plate2Clean == true)
        {
            if (other.tag == "Light")
            {
                plate2Wet.SetActive(false);
                plate2Done = true;
            }

        }

        if (cuttingboardClean == true)
        {
            if (other.tag == "Light")
            {
                cuttingboardWet.SetActive(false);
                cuttingboardClean = true;
            }

        }

        if (spoon2Clean == true)
        {
            if (other.tag == "Light")
            {
                spoon2Wet.SetActive(false);
                spoon2Done = true;
            }

        }

        if (bowl1Clean == true)
        {
            if (other.tag == "Light")
            {
                bowl1Wet.SetActive(false);
                bowl1Done = true;
            }

        }

        if (bowl2Clean == true)
        {
            if (other.tag == "Light")
            {
                bowl2Wet.SetActive(false);
                bowl2Done = true;
            }

        }

    }
}
