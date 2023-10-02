using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;

public class Cam1Record : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraPrefab;

    [SerializeField]
    private Transform CamPos;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use CompareTag for better performance
        {
            Debug.Log("Spawn Camera 1");
            GameObject camera = Instantiate(cameraPrefab, CamPos.position, CamPos.rotation);
            Debug.Log("Player Start Record Cam1");
            VideoCaptureCtrl.instance.StartCapture();
            Destroy(this.gameObject);
        }
    }
}
