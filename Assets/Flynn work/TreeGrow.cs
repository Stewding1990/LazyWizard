using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{

    public GameObject tree;
    public GameObject pot;
    
    public static bool watered;

    public Vector3 targetscale = new Vector3(0.05f, 0.05f, 0.05f);
    public float speed = 0.0001f;

    


    public GameObject potDarker1;
    public GameObject potDarker2;
    public GameObject potDarker3;
    public GameObject potDarker4;


    // Start is called before the first frame update
    void Start()
    {
        watered = false;

        potDarker1.SetActive(false);
        potDarker2.SetActive(false);
        potDarker3.SetActive(false);
        potDarker4.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (watered == true)
        {
            potDarker1.SetActive(true);
        }
     
    }


    private void OnTriggerStay(Collider other)
    {
        if (watered == true)
        { 
            if(other.tag == "Light")
            {
                tree.transform.localScale = Vector3.Lerp(tree.transform.localScale, targetscale, speed * Time.deltaTime);
                Debug.Log("triggered the flower");
            }
        
            
        }
    }
}
