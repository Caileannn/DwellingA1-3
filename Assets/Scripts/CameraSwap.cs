using Minis;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[ExecuteInEditMode]
[System.Serializable]
public class CamerSwap : MonoBehaviour
{

    public List<Camera> cameras;
    private float count;
    private bool b_R = false;
    private bool b_L = false;
    private bool m_Bool = false;

    void Start()
    {
        count = 0f;

        InputSystem.onDeviceChange += (device, change) =>
        {

            var num_of_cameras = cameras.Count;


            if (change != InputDeviceChange.Added)
            {

                return;
            }

            var midiDevice = device as Minis.MidiDevice;

            if (midiDevice == null)
            {

                return;
            }

            midiDevice.onWillControlChange += (device, change) =>
            {
                // print(device.controlNumber);

                if(device.controlNumber == 43)
                {

                    if(b_L) 
                    {
                        b_L = false;
                        return;
                    }
                        
                    count++;
                    if(count >= num_of_cameras)
                    {
                        count = 0;
                        Debug.Log(count);
                    }

                    b_L = true;

                   
                }
                if (device.controlNumber == 44)
                {

                    if (b_R)
                    {
                        b_R = false;
                        return;
                    }

                    count--;
                    if (count < 0)
                    {
                        count = num_of_cameras - 1;
                        Debug.Log(count);
                    }

                    b_R = true;

                    
                }
            };

            // Debug.Log(count);
        };

    }

    void Update()
    {
        var num_of_cameras = cameras.Count;
        
        var tc = 0;
       
        // Check if the input is a numeric key (0-9)
        if (Input.inputString.Length > 0 && char.IsDigit(Input.inputString[0]))
        {
            // Parse the input as an integer
            int inputNumber = int.Parse(Input.inputString[0].ToString());

            // Check if the input number is within the range 0-9
            if (inputNumber >= 0 && inputNumber <= 9)
            {
                tc = inputNumber;
                if (m_Bool)
                {
                    tc += 10;
                }

                // Iterate through the cameras array
                for (int i = 0; i < cameras.Count; i++)
                {
                    if (i == tc)
                    {
                        cameras[i].enabled = true;
                    }
                    else
                    {
                        cameras[i].enabled = false;
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(m_Bool)
            {
                m_Bool = false;
            }
            else
            {
                m_Bool = true;
            }

            Debug.Log(m_Bool);
        }






        // Debug.Log(m_Slider);

        //foreach (var item in cameras)
        //{

        //    if (tc == count
        //    {
        //        item.enabled = true;
        //    }
        //    else
        //    {
        //        item.enabled = false;
        //    }
        //    tc++;
        //}

    }
}
