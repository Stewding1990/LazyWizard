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

    public float VibDuration;
    public float VibStrength;


    //public GameObject waterTrigger;



    // Start is called before the first frame update
    void Start()
    {
        //waterTrigger.SetActive(false);

        spark.Stop();
        Wlight.Stop();
    }

    // Update is called once per frame
    void Update()
    {
       // if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
       // {
        //    spark.Play();
        //    Wlight.Play();

            if (originTop.transform.position.y < originBottom.transform.position.y)
            {
                //waterTrigger.SetActive(false);

                spark.Play();
                Wlight.Play();

                RaycastHit hit;
                if (Physics.Raycast(originTop.transform.position, Vector3.down, out hit))
                {
                    Debug.DrawLine(originTop.transform.position, hit.point, Color.cyan);

                    if (hit.collider.tag == "Plant1")
                    {
                        
                        TreeGrow.watered1 = true;
                    }

                    if (hit.collider.tag == "Plant2")
                    {
                        
                        TreeGrow.watered2 = true;
                    }

                    if (hit.collider.tag == "Plant3")
                    {
                        
                        TreeGrow.watered3 = true;
                    }

                    if (hit.collider.tag == "Plant4")
                    {
                        
                        TreeGrow.watered4 = true;
                    }



                    if (hit.collider.tag == "spoon1")
                    {
                        DishTable.spoon1Clean = true;
                    }

                    if (hit.collider.tag == "plate1")
                    {
                        DishTable.plate1Clean = true;
                    }

                    if (hit.collider.tag == "plate2")
                    {
                        DishTable.plate2Clean = true;
                    }

                    if (hit.collider.tag == "cuttingboard")
                    {
                        DishTable.cuttingboardClean = true;
                    }

                    if (hit.collider.tag == "spoon2")
                    {
                        DishTable.spoon2Clean = true;
                    }

                    if (hit.collider.tag == "bowl1")
                    {
                        DishTable.bowl1Clean = true;
                    }

                    if (hit.collider.tag == "bowl2")
                    {
                        DishTable.bowl2Clean = true;
                    }
                }
               
                drip.Play();
                SimpleHapticVibrationManager.VibrateController(VibDuration, VibStrength, OVRInput.Controller.RTouch);
            }
            else
            {
                //waterTrigger.SetActive(false);
                drip.Stop();

                spark.Stop();
                Wlight.Stop();
                SimpleHapticVibrationManager.VibrateController(0, 0, OVRInput.Controller.RTouch);
            }
           
       // }
       // else
       // {
        //    spark.Stop();
        //    Wlight.Stop();
        //}
    }
}
