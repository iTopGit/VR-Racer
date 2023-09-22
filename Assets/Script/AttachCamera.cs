using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
using RockVR.Video;
public class AttachCamera : MonoBehaviour
{

    //private Camera cam;
    //private VideoCaptureCtrl videoCaptureCtrl;


    private void OnTriggerEnter(Collider other)
    {
        Camera camera = other.GetComponentInChildren<Camera>();
        if (camera != null && camera.tag == "ThirdPersonCamera")
        {
            Debug.Log("Player Start Record");
            VideoCaptureCtrl.instance.StartCapture();
        }
    }
}
