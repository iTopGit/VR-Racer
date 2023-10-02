using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;


public class MoveCam : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform targetTransform;

    public bool isStart = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            if (objectToMove != null) {
                objectToMove.transform.position = targetTransform.position;
                objectToMove.transform.rotation = targetTransform.rotation;
            }

            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}