using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTriggerBoxes : MonoBehaviour
{
    public Transform[] TriggerPos;
    public GameObject[] triggerBoxes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use CompareTag for better performance
        {
            Debug.Log("Spawn a New trigger Box");

            for (int i = 0; i < triggerBoxes.Length; i++)
            {
                GameObject newTriggerBox = Instantiate(triggerBoxes[i], TriggerPos[i].position, TriggerPos[i].rotation);
            }

            StartCoroutine(ExampleCoroutine());
        }

    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}