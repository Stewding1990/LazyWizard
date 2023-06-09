using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{

    // candle stuff
    public GameObject IncompleteCandles;
    public GameObject CompleteCandles;



    public GameObject pot;
    
    public static bool watered1;
    public static bool watered2;
    public static bool watered3;
    public static bool watered4;

    public Vector3 targetscale = new Vector3(0.05f, 0.05f, 0.05f);
    public float speed = 0.0001f;


    bool tree1G = false;
    bool tree2G = false;
    bool tree3G = false;
    bool tree4G = false;

    public GameObject potDarker1;
    public GameObject potDarker2;
    public GameObject potDarker3;
    public GameObject potDarker4;

    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;


    // Start is called before the first frame update
    void Start()
    {
        watered1 = false;
        watered2 = false;
        watered3 = false;
        watered4 = false;

        potDarker1.SetActive(false);
        potDarker2.SetActive(false);
        potDarker3.SetActive(false);
        potDarker4.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (watered1 == true)
        {
            potDarker1.SetActive(true);
        }

        if (watered2 == true)
        {
            potDarker2.SetActive(true);
        }

        if (watered3 == true)
        {
            potDarker3.SetActive(true);
        }

        if (watered4 == true)
        {
            potDarker4.SetActive(true);
        }

        if (tree1G == true & tree2G == true & tree3G == true & tree4G == true)
        {
            IncompleteCandles.SetActive(false);
            CompleteCandles.SetActive(true);
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (watered1 == true)
        { 
            if(other.tag == "Light")
            {
                tree1.transform.localScale = Vector3.Lerp(tree1.transform.localScale, targetscale, speed * Time.deltaTime);  
            } 

            if (tree1.transform.localScale == targetscale)
            {
                tree1G = true;
            }
        }

        if (watered2 == true)
        {
            if (other.tag == "Light")
            {
                tree2.transform.localScale = Vector3.Lerp(tree2.transform.localScale, targetscale, speed * Time.deltaTime);
            }

            if (tree2.transform.localScale == targetscale)
            {
                tree2G = true;
            }
        }

        if (watered3 == true)
        {
            if (other.tag == "Light")
            {
                tree3.transform.localScale = Vector3.Lerp(tree3.transform.localScale, targetscale, speed * Time.deltaTime);
            }
            if (tree3.transform.localScale == targetscale)
            {
                tree3G = true;
            }
        }

        if (watered4 == true)
        {
            if (other.tag == "Light")
            {
                tree4.transform.localScale = Vector3.Lerp(tree4.transform.localScale, targetscale, speed * Time.deltaTime);
            }
            if (tree4.transform.localScale == targetscale)
            {
                tree4G = true;
            }
        }
    }
}
