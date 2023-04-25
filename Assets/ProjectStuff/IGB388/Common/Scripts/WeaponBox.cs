using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Windows.Forms.DataVisualization.Charting;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class WeaponBox : MonoBehaviour
{
    public int score;
    public string scoreText = "For this activity you need to clean up the weapons in this corner, you can do this by picking up all of the weapons and placing them in the open chest. You will be using a levitation spell to do this in the complete game, but for now, throw them in the chest. Weapons 0/5.";

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("WeaponsScore").GetComponent<TextMeshProUGUI>().text = ("For this activity you need to clean up the weapons in this corner, you can do this by picking up all of the staffs and placing them in the open chest. You will be using a levitation spell to do this in the complete game, but for now, throw them in the chest.");


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapons")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
            //other.gameObject.SetActive(false);
            score++;
            print(score);
        }
    }
}
