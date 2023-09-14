using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
public class AttachCamera : MonoBehaviour
{
    public Camera CameraToRecord;
    public string OutputPath;
    public bool IsRecording;
    public string CameraTag;

    void Start()
    {
        IsRecording = false;
    }
}
