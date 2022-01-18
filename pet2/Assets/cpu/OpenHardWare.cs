using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenHardwareMonitor.Hardware;
using System;
using UnityEngine.UI;

namespace Csharp_CPUTemp
{
    public class OpenHardWare : MonoBehaviour
    {
        private Text myCPU;
        // Start is called before the first frame update
        Computer c = new Computer();
        void Start()
        {
            myCPU = GameObject.Find("CPU_Temp(Text)").GetComponent<Text>();
            
            c.CPUEnabled = true;
            c.Open();

            foreach (var hardware in c.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensors in hardware.Sensors)
                    {
                        //Debug.Log(sensors.Name);
                        if (sensors.SensorType == SensorType.Load)
                        
                            if (sensors.Name == "CPU Total")
                            {
                                myCPU.text = sensors.Value.GetValueOrDefault().ToString() + "%";
                                Debug.Log(sensors.Name + ":" + sensors.Value.GetValueOrDefault());
                            } 
                    }

                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}