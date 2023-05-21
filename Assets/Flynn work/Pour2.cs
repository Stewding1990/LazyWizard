using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour2 : MonoBehaviour
{

    public ParticleSystem drip;
    public ParticleSystem spark;
    public ParticleSystem Wlight;

    public GameObject originTop;
    public GameObject originBottom;

    public GameObject waterTrigger;

    

    // Start is called before the first frame update
    void Start()
    {
        waterTrigger.SetActive(false);

        spark.Stop();
        Wlight.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            spark.Play();
            Wlight.Play();

            if (originTop.transform.position.y < originBottom.transform.position.y)
            {
                waterTrigger.SetActive(false);
                

                RaycastHit hit;
                if (Physics.Raycast(originTop.transform.position, Vector3.down, out hit))
                {
                    Debug.DrawLine(originTop.transform.position, hit.point, Color.cyan);

                    if (hit.collider.tag == "Plant")
                    {
                        Debug.Log("hit plant");
                        TreeGrow.watered = true;
                    }
                }
                




                drip.Play();
            }
            else
            {
                waterTrigger.SetActive(false);
                drip.Stop();
            }
           
        }
        else
        {
            spark.Stop();
            Wlight.Stop();
        }
    }
}
