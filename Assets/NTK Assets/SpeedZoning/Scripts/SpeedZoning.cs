using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZoning : MonoBehaviour
{
    // Set Zone Speed 
    public float speed_limit;
    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get Car Speed
            float carSpeed = other.GetComponent<CarController>().currentSpeed;
            if (carSpeed < speed_limit)
            {
                // Do Something
            }
        }
        
    }
}
