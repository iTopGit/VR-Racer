using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;


public class MoveCam : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform targetTransform;

    void OnTriggerEnter(Collider other)
    {
        VideoCaptureCtrl.instance.StartCapture();
        objectToMove.transform.position = targetTransform.position;
        objectToMove.transform.rotation = targetTransform.rotation;
    }
}