using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadsetDetect : MonoBehaviour
{
    void Start() 
    {
        if (XRSettings.isDeviceActive && XRSettings.loadedDeviceName != "MockHMD Display") 
        {
            gameObject.SetActive(false);
        }
    }
}
