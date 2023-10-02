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
            for (int i = 0; i < triggerBoxes.Length; i++) {
                if(i == 0) { moveInstance(i); } else { createInstance(i); }
            }
            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine() {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void createInstance(int index) {
        Instantiate(triggerBoxes[index], TriggerPos[index].position, TriggerPos[index].rotation);
    }

    void moveInstance(int index) {
        triggerBoxes[index].transform.position = TriggerPos[index].transform.position;
        triggerBoxes[index].transform.rotation = TriggerPos[index].transform.rotation;
    }
}
