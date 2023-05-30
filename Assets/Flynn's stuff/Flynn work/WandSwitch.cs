using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSwitch : MonoBehaviour
{
    public GameObject wandParrent;
    public float moveRange = 1.0f; 
    public float moveSpeed = 1.0f;
    public ParticleSystem wind;

    public GameObject waterWand;
    public GameObject lightWand;
    //public GameObject fireWand;
    //public GameObject levitationWand;

    Rigidbody rigidbody;

    private Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        waterWand.SetActive(true);
        lightWand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if not grabbed activate wind effect and floating movement
        if (Input.GetKey("up"))
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rigidbody.constraints = RigidbodyConstraints.None;

            currentPos = wandParrent.transform.position;

            // Calculate a random offset within the specified range
            float xOffset = Mathf.PerlinNoise(Time.time * moveSpeed, 0.0f) * moveRange - moveRange / 2.0f;
            float yOffset = Mathf.PerlinNoise(0.0f, Time.time * moveSpeed) * moveRange - moveRange / 2.0f;
            float zOffset = Mathf.PerlinNoise(Time.time * moveSpeed, Time.time * moveSpeed) * moveRange - moveRange / 2.0f;

            // Update the position of the GameObject with the random offset
            wandParrent.transform.position = currentPos + new Vector3(xOffset, yOffset, zOffset);
            wind.Play();
        }
        else
        {
            wind.Stop();
        }

        
    }

    public void WaterWandSet()
    {
        waterWand.SetActive(true);
        lightWand.SetActive(false);
        //fireWand.SetActive(false);
        //levitationWand.SetActive(false);
    }

    public void LightWandSet()
    {
        waterWand.SetActive(false);
        lightWand.SetActive(true);
        //fireWand.SetActive(false);
        //levitationWand.SetActive(false);
    }

    public void LevitationWandSet()
    {
        waterWand.SetActive(false);
        lightWand.SetActive(false);
        //fireWand.SetActive(false);
        //levitationWand.SetActive(true);
    }

    public void FireWandSet()
    {
        waterWand.SetActive(false);
        lightWand.SetActive(false);
        //fireWand.SetActive(true);
        //levitationWand.SetActive(false);
    }
}
