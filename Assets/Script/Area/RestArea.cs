using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestArea : MonoBehaviour
{
    private NPCCar car;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "Car" && other.transform.GetComponent<NPCCar>().fromSpawnNumber == 1)
        {
            car = other.transform.GetComponent<NPCCar>();
          
            car.StopCar();
        }
    }

    public void ReleaseCar()
    {
        if (car != null)
        {
            car.ReleaseCar();
        }
    }
}
