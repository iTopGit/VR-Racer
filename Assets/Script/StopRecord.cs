using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;
public class StopRecord : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use CompareTag for better performance
        {
            Debug.Log("Player Stop Record");
            VideoCaptureCtrl.instance.StopCapture();
        }
    }
}
