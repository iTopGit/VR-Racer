using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class CameraAutoRecord : MonoBehaviour
{
    public string taggedCameraTag = "TaggedCamera";
    public string recordingsFolder = "Recordings";
    public int frameRate = 30;

    private bool isRecording = false;

    void Start()
    {
        Application.targetFrameRate = frameRate;
        Time.captureFramerate = frameRate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRecording = !isRecording;

            if (isRecording)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }
    }

    void StartRecording()
    {
        // Create the recordings folder if it doesn't exist
        System.IO.Directory.CreateDirectory(recordingsFolder);

        // Start capturing frames
        string outputFile = $"{recordingsFolder}/Recording_{System.DateTime.Now:yyyyMMddHHmmss}.mp4";
        string screenCaptureCommand = $"-i UnityCapturedFrames/frame_%04d.png -r {frameRate} -y -c:v libx264 -pix_fmt yuv420p {outputFile}";

        Process.Start("ffmpeg", screenCaptureCommand);

        //Debug.Log("Recording started.");
    }

    void StopRecording()
    {
        // Stop capturing frames (no need to do anything here)
        //Debug.Log("Recording stopped.");
    }
}
