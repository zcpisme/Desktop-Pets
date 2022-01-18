using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mypos : MonoBehaviour
{

    //private GameObject main_Cam;
    //private GameObject Cam1;
    private GameObject myMute;
    private GameObject myMemory;
    private GameObject myWeather;
    //private Vector3 initialPosition;
    void Start()
    {
        
        //main_Cam = GameObject.Find("Main Camera");
        //initialPosition = main_Cam.transform.position;
        //Cam1 = GameObject.Find("Camera");
        //Cam1.SetActive(false);
        myMute = GameObject.Find("Mute(Toggle)");
        myMute.SetActive(false);
        myMemory = GameObject.Find("avaMemory(Toggle)");
        myMemory.SetActive(false);
        myWeather = GameObject.Find("Weather(Toggle)");
        myWeather.SetActive(false);
    }

    bool turnleft = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (turnleft)
            {
                // transform.position = transform.position + new Vector3((float)-4.7, 0, 0);
     //           Cam1.SetActive(true);
   //             main_Cam.SetActive(false);
                //main_Cam.transform.position = initialPosition + new Vector3((float)4.35, 0, 0);
                turnleft = false;
                myMute.SetActive(true);
                myMemory.SetActive(true);
                myWeather.SetActive(true);
            }
            else 
            {
                //transform.position = initialPosition;
                //Cam1.SetActive(false);
                //main_Cam.SetActive(true);
                //main_Cam.transform.position = initialPosition;
                turnleft = true;
                myMute.SetActive(false);
                myMemory.SetActive(false);
                myWeather.SetActive(false);
            }
            
        }
    }
}
