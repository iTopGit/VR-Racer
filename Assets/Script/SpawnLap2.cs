using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLap2 : MonoBehaviour
{

    
    public GameObject objectToMove;
    public Transform targetTransform;
    public float speed = 1.0f;
    public float Delay;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Move Camera");
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);

        // Rotate the object to match the rotation of the target transform.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, speed * Time.deltaTime);

        StartCoroutine(DestroyAfter10SecondsCoroutine());
    }

    IEnumerator DestroyAfter10SecondsCoroutine()
    {
        // Wait for 10 seconds.
        yield return new WaitForSeconds(Delay);

        // Destroy the object.
        Destroy(gameObject);
    }
}