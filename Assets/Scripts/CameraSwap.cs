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
        // Debug.Log(m_Slider);
        var tc = 0;
        foreach (var item in cameras)
        {

            if (tc == count)
            {
                item.enabled = true;
            }
            else
            {
                item.enabled = false;
            }
            tc++;
        }

    }
}
